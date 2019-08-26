## 1. A guide to AssetBundles and Resources

​	This is a series of articles that provides an in-depth discussion of Assets and resource management in the Unity engine. It seeks to provide expert developers with deep, source-level knowledge of Unity's Asset and serialization systems. It examines both the technical underpinnings of Unity's AssetBundle system and the current best practices for employing them.

​	The guide is broken down into four chapters:

1. **Assets, Objects and serialization** discusses the low-level details of how Unity serializes Assets and handles references between Assets. *It is strongly recommended that readers begin with this chapter* as it defines terminology used throughout the guide.
2. **The Resources folder** discusses the built-in Resources API.
3. **AssetBundle fundamentals** builds on the information in chapter 1 to describe how AssetBundles operate, and discusses both the loading of AssetBundles and the loading of Assets from AssetBundles.
4. **AssetBundle usage patterns** is a long article discussing many of the topics surrounding the practical uses of AssetBundles. It includes sections on assigning Assets to AssetBundles and on managing loaded Assets, and describes many common pitfalls encountered by developers using AssetBundles.

>  *Note:* This guide's terms for *Objects* and *Assets* differ from Unity's public API naming conventions.

​	The data this guide calls *Objects* are called *Assets* in many public Unity APIs, such as[ AssetBundle.LoadAsset](http://docs.unity3d.com/ScriptReference/AssetBundle.LoadAsset.html) and[ Resources.UnloadUnusedAssets](http://docs.unity3d.com/ScriptReference/Resources.UnloadUnusedAssets.html). The files this guide calls *Assets* are rarely exposed to any public APIs. When they are exposed, it is generally only in build-related code, such as[ AssetDatabase](http://docs.unity3d.com/ScriptReference/AssetDatabase.html) and[ BuildPipeline](http://docs.unity3d.com/ScriptReference/BuildPipeline.html). In these cases, they are called *files* in public APIs.

## 2. Assets, Objects and serialization

​	This chapter covers the deep internals of Unity's serialization system and how Unity maintains robust references between different Objects, both in the Unity Editor and at runtime. It also discusses the technical distinctions between Objects and Assets. The topics covered here are fundamental to understanding how to efficiently load and unload Assets in Unity. Proper Asset management is crucial to keeping loading times short and memory usage low.

### 2.1. Inside Assets and Objects

​	To understand how to properly manage data in Unity, it is important to understand how Unity identifies and serializes data. The first key point is the distinction between ***Assets*** and ***UnityEngine.Objects***.

​	An ***Asset*** is a file on disk, stored in the *Assets* folder of a Unity project. Textures, 3D models, or audio clips are common types of Assets. Some Assets contain data in formats native to Unity, such as materials. Other Assets need to be processed into native formats, such as FBX files.

​	A ***UnityEngine.Object***, or ***Object*** with a capitalized 'O', is a set of serialized data collectively describing a specific instance of a resource. This can be any type of resource which the Unity Engine uses, such as a mesh, sprite, AudioClip or AnimationClip. All Objects are subclasses of the[ UnityEngine.Object](http://docs.unity3d.com/ScriptReference/Object.html) base class.

​	While most Object types are built-in, there are two special types.

1. A[ ScriptableObject](http://docs.unity3d.com/ScriptReference/ScriptableObject.html) provides a convenient system for developers to define their own data types. These types can be natively serialized and deserialized by Unity, and manipulated in the Unity Editor's Inspector window.

2. A[ MonoBehaviour](http://docs.unity3d.com/ScriptReference/MonoBehaviour.html) provides a wrapper that links to a[ MonoScript](http://docs.unity3d.com/ScriptReference/MonoScript.html). A MonoScript is an internal data type that Unity uses to hold a reference to a specific scripting class within a specific assembly and namespace. The MonoScript does *not* contain any actual executable code.

   There is a one-to-many relationship between Assets and Objects; that is, any given Asset file contains one or more Objects.

### 2.2. Inter-Object references

​	All UnityEngine.Objects can have references to other UnityEngine.Objects. These other Objects may reside within the same Asset file, or may be imported from other Asset files. For example, a material Object usually has one or more references to texture Objects. These texture Objects are generally imported from one or more texture Asset files (such as PNGs or JPGs).

​	When serialized, these references consist of two separate pieces of data: a ***File GUID*** and a ***Local ID***. The File GUID identifies the Asset file where the target resource is stored. A locally unique Local ID identifies each Object within an Asset file because an Asset file may contain multiple Objects. (Note: AA Local ID is unique from all the other Local IDs for the same Asset file.)

​	File GUIDs are stored in .meta files. These .meta files are generated when Unity first imports an Asset, and are stored in the same directory as the Asset.

​	The above identification and referencing system can be seen in a text editor: create a fresh Unity project and change its Editor Settings to expose Visible Meta Files and to serialize Assets as text. Create a material and import a texture into the project. Assign the material to a cube in the scene and save the scene.

​	Using a text editor, open the .meta file associated with the material. A line labeled "guid" will appear near the top of the file. This line defines the material Asset's File GUID. To find the Local ID, open the material file in a text editor. The material Object's definition will look like this:

```
--- !u!21 &2100000
Material:
 serializedVersion: 3
 ... more data …
```

​	In the above example, the number preceded by an ampersand is the material's Local ID. If this material Object were located inside an Asset identified by the File GUID "abcdefg", then the material Object could be uniquely identified as the combination of the File GUID "abcdefg" and the Local ID "2100000".

### 2.3. Why File GUIDs and Local IDs?

​	Why is Unity's File GUID and Local ID system necessary? The answer is robustness and to provide a flexible, platform-independent workflow.

​	The File GUID provides an abstraction of a file's specific location. As long as a specific File GUID can be associated with a specific file, that file's location on disk becomes irrelevant. The file can be freely moved without having to update all Objects referring to the file.

​	As any given Asset file may contain (or produce via import) multiple UnityEngine.Object resources, a Local ID is required to unambiguously distinguish each distinct Object.

If the File GUID associated with an Asset file is lost, then references to all Objects in that Asset file will also be lost. This is why it is important that the .meta files must remain stored with the same file names and in the same folders as their associated Asset files. Note that Unity will regenerate deleted or misplaced .meta files.

The Unity Editor has a map of specific file paths to known File GUIDs. A map entry is recorded whenever an Asset is loaded or imported. The map entry links the Asset's specific path to the Asset's File GUID. If the Unity Editor is open when a .meta file goes missing and the Asset's path does not change, the Editor can ensure that the Asset retains the same File GUID.

If the .meta file is lost while the Unity Editor is closed, or the Asset's path changes without the .meta file moving along with the Asset, then all references to Objects within that Asset will be broken.

### 2.4. Composite Assets and importers

As mentioned in the Inside Assets and Objects section, non-native Asset types must be imported into Unity. This is done via an asset importer. While these importers are usually invoked automatically, they are also exposed to scripts via the [AssetImporter](http://docs.unity3d.com/ScriptReference/AssetImporter.html) API. For example, the [TextureImporter](http://docs.unity3d.com/ScriptReference/TextureImporter.html) API provides access to the settings used when importing individual texture Assets, such as PNG files.

The result of the import process is one or more UnityEngine.Objects. These are visible in the Unity Editor as multiple sub-assets within the parent Asset, such as multiple sprites nested beneath a texture Asset that has been imported as a sprite atlas. Each of these Objects will share a File GUID as their source data is stored within the same Asset file. They will be distinguished within the imported texture Asset by a Local ID.

The import process converts source Assets into formats suitable for the target platform selected in the Unity Editor. The import process can include a number of heavyweight operations, such as texture compression. As this is often a time-consuming process, imported Asset are cached in the *Library* folder, eliminating the need to re-import Assets again on the next Editor launch.

Specifically, the results of the import process are stored in a folder named for the first two digits of the Asset's File GUID. This folder is stored inside the *Library/metadata/* folder. The individual Objects from the Asset are serialized into a single binary file that has a name identical to the Asset's File GUID.

This process applies to *all* Assets, not just non-native Assets. Native assets do not require lengthy conversion processes or re-serialization.

### 2.5. Serialization and instances

While File GUIDs and Local IDs are robust, GUID comparisons are slow and a more performant system is needed at runtime. Unity internally maintains a cache that translates File GUIDs and Local IDs into simple, session-unique integers (note: Internally, this cache is called the PersistentManager.) These integers are called Instance IDs, and are assigned in a simple, monotonically-increasing order when new Objects are registered with the cache.

The cache maintains mappings between a given Instance ID, File GUID and Local ID defining the location of the Object's source data, and the instance of the Object in memory (if any). This allows UnityEngine.Objects to robustly maintain references to each other. Resolving an Instance ID reference can quickly return the loaded Object represented by the Instance ID. If the target Object is not yet loaded, the File GUID and Local ID can be resolved to the Object's source data, allowing Unity to load the object just-in-time.

At startup, the Instance ID cache is initialized with data for all Objects immediately required by the project (i.e., referenced in built Scenes), as well as all Objects contained in the Resources folder. Additional entries are added to the cache when new assets are imported at runtime and when Objects are loaded from AssetBundles (note: An example of an Asset created at runtime would be a Texture2D Object created in script, like so: var myTexture = new Texture2D(1024, 768);). Instance ID entries are only removed from the cache when an AssetBundle providing access to a specific File GUID and Local ID is unloaded. When this occurs, the mapping between the Instance ID, its File GUID and Local ID are deleted to conserve memory. If the AssetBundle is re-loaded, a *new* Instance ID will be created for each Object loaded from the re-loaded AssetBundle.

For a deeper discussion of the implications of unloading AssetBundles, see the **Managing Loaded Assets** section in the **AssetBundle Usage Patterns** step.

On specific platforms, certain events can force Objects out of memory. For example, graphical Assets can be unloaded from graphics memory on iOS when an app is suspended. If these Objects originated in an AssetBundle that has been unloaded, Unity will be unable to reload the source data for the Objects. Any extant references to these Objects will also be invalid. In the preceding example, the scene may appear to have invisible meshes or magenta textures.

*Implementation note:* At runtime, the above control flow is not literally accurate. Comparing File GUIDs and Local IDs at runtime would not be sufficiently performant during heavy loading operations. When building a Unity project, the File GUIDs and Local IDs are deterministically mapped into a simpler format. However, the concept remains identical, and thinking in terms of File GUIDs and Local IDs remains a useful analogy during runtime. This is also the reason why Asset File GUIDs cannot be queried at runtime.

### 2.6. MonoScripts

It is important to understand that a MonoBehaviour has a reference to a MonoScript, and MonoScripts simply contain the information needed to *locate* a specific script class. Neither type of Object contains the executable code of script class.

A MonoScript contains three strings: assembly name, class name, and namespace.

While building a project, Unity compiles all the loose script files in the Assets folder into Mono assemblies. C# scripts outside of the *Plugins* subfolder are placed into *Assembly-CSharp.dll*. Scripts within the *Plugins* subfolder are placed into *Assembly-CSharp-firstpass.dll*, and so on. In addition, Unity 2017.3 also introduces the ability to [define custom managed assemblies](https://docs.unity3d.com/Manual/ScriptCompilationAssemblyDefinitionFiles.html).

These assemblies, as well as pre-built assembly DLL files, are included in the final build of a Unity application. They are also the assemblies to which a MonoScript refers. Unlike other resources, all assemblies included in a Unity application are loaded on application start-up.

This MonoScript Object is the reason why an AssetBundle (or a Scene or a prefab) does not actually contain executable code in any of the MonoBehaviour Components in the AssetBundle, Scene or prefab. This allows different MonoBehaviours to refer to specific shared classes, even if the MonoBehaviours are in different AssetBundles.

### 2.7. Resource lifecycle

To reduce loading times and manage an application's memory footprint, it's important to understand the resource lifecycle of UnityEngine.Objects. Objects are loaded into/unloaded from memory at specific and defined times.

An Object is loaded automatically when:

1. The Instance ID mapped to that Object is dereferenced
2. The Object is currently not loaded into memory
3. The Object's source data can be located.

Objects can also be explicitly loaded in scripts, either by creating them or by calling a resource-loading API (e.g., [AssetBundle.LoadAsset](http://docs.unity3d.com/ScriptReference/AssetBundle.LoadAsset.html)). When an Object is loaded, Unity tries to resolve any references by translating each reference's File GUID and Local ID into an Instance ID. An Object will be loaded on-demand the first time its Instance ID is dereferenced if two criteria are true:

1. The Instance ID references an Object that is not currently loaded
2. The Instance ID has a valid File GUID and Local ID registered in the cache

This generally occurs very shortly after the reference itself is loaded and resolved.

If a File GUID and Local ID do not have an Instance ID, or if an Instance ID with an unloaded Object references an invalid File GUID and Local ID, then the reference is preserved but the actual Object will not be loaded. This appears as a "(Missing)" reference in the Unity Editor. In a running application, or in the Scene View, "(Missing)" Objects will be visible in different ways, depending on their types. For example, meshes will appear to be invisible, while textures may appear to be magenta.

Objects are unloaded in three specific scenarios:

- Objects are automatically unloaded when unused Asset cleanup occurs. This process is triggered automatically when scenes are changed destructively (i.e. when [SceneManager.LoadScene](https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.html) is invoked non-additively), or when a script invokes the[ Resources.UnloadUnusedAssets](http://docs.unity3d.com/ScriptReference/Resources.UnloadUnusedAssets.html) API. This process only unloads unreferenced Objects; an Object will only be unloaded if no Mono variable holds a reference to the Object, and there are no other live Objects holding references to the Object. Furthermore, note that anything marked with [HideFlags.DontUnloadUnusedAsset](https://docs.unity3d.com/ScriptReference/HideFlags.DontUnloadUnusedAsset.html) and [HideFlags.HideAndDontSave](https://docs.unity3d.com/ScriptReference/HideFlags.HideAndDontSave.html) will not be unloaded.
- Objects sourced from the Resources folder can be explicitly unloaded by invoking the[ Resources.UnloadAsset](http://docs.unity3d.com/ScriptReference/Resources.UnloadAsset.html) API. The Instance ID for these Objects remains valid and will still contain a valid File GUID and LocalID entry. If any Mono variable or other Object holds a reference to an Object that is unloaded with[ Resources.UnloadAsset](http://docs.unity3d.com/ScriptReference/Resources.UnloadAsset.html), then that Object will be reloaded as soon as any of the live references are dereferenced.
- Objects sourced from AssetBundles are automatically and immediately unloaded when invoking the[ AssetBundle.Unload](http://docs.unity3d.com/ScriptReference/AssetBundle.Unload.html)(true) API. This invalidates the File GUID and Local ID of the Object's Instance ID, and any live references to the unloaded Objects will become "(Missing)" references. From C# scripts, attempting to access methods or properties on an unloaded object will produce a *NullReferenceException*.

If [AssetBundle.Unload](http://docs.unity3d.com/ScriptReference/AssetBundle.Unload.html)(false) is called, live Objects sourced from the unloaded AssetBundle will not be destroyed, but Unity will invalidate the File GUID and Local ID references of their Instance IDs. It will be impossible for Unity to reload these Objects if they are later unloaded from memory and live references to the unloaded Objects remain. 

(Note: The most common case where Objects are removed from memory at runtime without being unloaded occurs when Unity loses control of its graphics context. This may occur when a mobile app is suspended and the app is forced into the background. In this case, the mobile OS usually evicts all graphical resources from GPU memory. When the app returns to the foreground, Unity must reload all needed Textures, Shaders and Meshes to the GPU before scene rendering can resume.)

### 2.8. Loading large hierarchies

When serializing hierarchies of Unity GameObjects, such as during prefabs serialization, it is important to remember that the entire hierarchy will be fully serialized. That is, every GameObject and Component in the hierarchy will be individually represented in the serialized data. This has interesting impacts on the time required to load and instantiate hierarchies of GameObjects.

When creating any GameObject hierarchy, CPU time is spent in several different ways:

- Reading the source data (from storage, from an AssetBundle, from another GameObject, etc.)
- Setting up the parent-child relationships between the new Transforms
- Instantiating the new GameObjects and Components
- Awakening the new GameObjects and Components on the main thread

The latter three time costs are generally invariant regardless of whether the hierarchy is being cloned from an existing hierarchy or is being loaded from storage. However, the time to read the source data increases linearly with the number of Components and GameObjects serialized into the hierarchy, and is also multiplied by the speed of the data source.

On all current platforms, it is considerably faster to read data from elsewhere in memory rather than loading it from a storage device. Further, the performance characteristics of the available storage media vary widely between different platforms. Therefore, when loading prefabs on platforms with slow storage, the time spent reading the prefab's serialized data from storage can rapidly exceed the time spent instantiating the prefab. That is, the cost of the loading operation is bound to storage I/O time.

As mentioned before, when serializing a monolithic prefab, every GameObject and component's data is serialized separately, which may duplicate data. For example, a UI screen with 30 identical elements will have the identical element serialized 30 times, producing a large blob of binary data. At load time, the data for all of the GameObjects and Components on each one of those 30 duplicate elements must be read from disk before being transferred to the newly-instantiated Object. This file reading time is a significant contributor to the overall cost of instantiating large prefabs. Large hierarchies should be instantiated in modular chunks, and then be stitched together at runtime.

*Unity 5.4 note:* Unity 5.4 altered the representation of transforms in memory. Each root transform's entire child hierarchy is stored in compact, contiguous regions of memory. When instantiating new GameObjects that will be instantly reparented into another hierarchy, consider using the new [GameObject.Instantiate](https://docs.unity3d.com/ScriptReference/Object.Instantiate.html) overloaded variants which accept a parent argument. Using this overload avoids the allocation of a root transform hierarchy for the new GameObject. In tests, this speeds up the time required for an instantiate operation by about 5-10%.

## 3. The Resources folder

This chapter discusses the *Resources* system. This is the system that allows developers to store Assets within one or more folders named *Resources* and to load or unload Objects from those Assets at runtime using the[ Resources](http://docs.unity3d.com/ScriptReference/Resources.html) API.

### 3.1. Best Practices for the Resources System

***Don't use it.***

This strong recommendation is made for several reasons:

- Use of the Resources folder makes fine-grained memory management more difficult
- Improper use of Resources folders will increase application startup time and the length of builds
- As the number of Resources folders increases, management of the Assets within those folders becomes very difficult
- The Resources system degrades a project's ability to deliver custom content to specific platforms and eliminates the possibility of incremental content upgrades
- AssetBundle Variants are Unity's primary tool for adjusting content on a per-device basis

### 3.2. Proper uses of the Resources system

There are two specific use cases where the Resources system can be helpful without impeding good development practices:

1. The ease of the Resources folder makes it an excellent system to rapidly prototype. However, when a project moves into full production, the use of the Resources folder should be eliminated.
2. The Resources folder may be useful in some trivial cases, if the content is:
3. Generally required throughout a project's lifetime
4. Not memory-intensive
5. Not prone to patching, or does not vary across platforms or devices
6. Used for minimal bootstrapping

Examples of this second case include MonoBehaviour singletons used to host prefabs, or ScriptableObjects containing third-party configuration data, such as a Facebook App ID.

### 3.3. Serialization of Resources

The Assets and Objects in all folders named "Resources" are combined into a single serialized file when a project is built. This file also contains metadata and indexing information, similar to an AssetBundle. As described in the [AssetBundle documentation](https://docs.unity3d.com/Manual/AssetBundlesIntro.html), this index includes a serialized lookup tree that is used to resolve a given Object's name into its appropriate File GUID and Local ID. It is also used to locate the Object at a specific byte offset in the serialized file's body.

On most platforms, the lookup data structure is a balanced search tree, which has a construction time that grows at an O(n log(n)) rate. This growth also causes the index's loading time to grow more-than-linearly as the number of Objects in Resources folders increases.

This operation is unskippable and occurs at application startup time while the initial non-interactive splash screen is displayed. Initializing a Resources system containing 10,000 assets has been observed to consume multiple seconds on low-end mobile devices, even though most of the Objects contained in Resources folders are rarely actually needed to load into an application's first scene.

## 4. AssetBundle fundamentals

This chapter discusses AssetBundles. It introduces the fundamental systems upon which AssetBundles are built, as well as the core APIs used to interact with AssetBundles. In particular, it discusses both the loading and unloading of AssetBundles themselves, as well as the loading and unloading of specific Asset and Objects from AssetBundles.

For more patterns and best practices on the uses of AssetBundles, see the next chapter in this series.

### 4.1. Overview

The AssetBundle system provides a method for storing one or more files in an archival format that Unity can index and serialize. AssetBundles are Unity's primary tool for the delivery and updating of non-code content after installation. This permits developers to submit a smaller app package, minimize runtime memory pressure, and selectively load content optimized for the end-user's device.

Understanding the way AssetBundles work is essential to building a successful Unity project for mobile devices. For an overall description of AssetBundle contents, review the [AssetBundle documentation](https://docs.unity3d.com/Manual/AssetBundlesIntro.html).

### 4.2. AssetBundle layout

To summarize, an AssetBundle consists of two parts: a header and data segment.

The header contains information about the AssetBundle, such as its identifier, compression type, and a manifest. The manifest is a lookup table keyed by an Object's name. Each entry provides a byte index that indicates where a given Object can be found within the AssetBundle's data segment. On most platforms, this lookup table is implemented as a balanced search tree. Specifically, Windows and OSX-derived platforms (including iOS) employ a red-black tree. Therefore, the time needed to construct the manifest will increase *more than linearly* as the number of Assets within an AssetBundle grows.

The data segment contains the raw data generated by serializing the Assets in the AssetBundle. If LZMA is specified as the compression scheme, the complete byte array for all serialized assets is compressed. If LZ4 is instead specified, bytes for separate Assets are individually compressed. If no compression is used, the data segment will remain as raw byte streams.

Prior to Unity 5.3, Objects could not be compressed individually inside an AssetBundle. As a consequence, if a version of Unity before 5.3 is instructed to read one or more Objects from a compressed AssetBundle, Unity had to decompress the entire AssetBundle. Generally, Unity cached a decompressed copy of the AssetBundle to improve loading performance for subsequent loading requests on the same AssetBundle.

### 4.3. Loading AssetBundles

AssetBundles can be loaded via four distinct APIs. The behavior of these four APIs is different depending on two criteria:

1. Whether the AssetBundle is LZMA compressed, LZ4 compressed or uncompressed
2. The platform on which the AssetBundle is being loaded

These APIs are:

- AssetBundle.LoadFromMemory(Async optional)
- AssetBundle.LoadFromFile(Async optional)
- UnityWebRequest's DownloadHandlerAssetBundle
- WWW.LoadFromCacheOrDownload (on Unity 5.6 or older)

#### 4.3.1 AssetBundle.LoadFromMemory(Async)

***Unity's recommendation is not to use this API.***

[AssetBundle.LoadFromMemoryAsync](https://docs.unity3d.com/ScriptReference/AssetBundle.LoadFromMemoryAsync.html) loads an AssetBundle from a managed-code byte array (*byte[]* in C#). It will always copy the source data from the managed-code byte array into a newly-allocated, contiguous block of native memory. If the AssetBundle is LZMA compressed, it will decompress the AssetBundle while copying. Uncompressed and LZ4-compressed AssetBundles will be copied verbatim.

The peak amount of memory consumed by this API will be at least twice the size of the AssetBundle: one copy in native memory created by the API, and one copy in the managed byte array passed to the API. Assets loaded from an AssetBundle created via this API will therefore be duplicated *three* times in memory: once in the managed-code byte array, once in the native-memory copy of the AssetBundle and a third time in GPU or system memory for the asset itself.

*Prior to Unity 5.3.3, this API was known as **AssetBundle.CreateFromMemory**. Its functionality has not changed.*

#### 4.3.2. AssetBundle.LoadFromFile(Async)

[AssetBundle.LoadFromFile](https://docs.unity3d.com/ScriptReference/AssetBundle.LoadFromFile.html) is a highly-efficient API intended for loading uncompressed or LZ4-compressed AssetBundle from local storage, such as a hard disk or an SD card.

On desktop standalone, console, and mobile platforms, the API will only load the AssetBundle's header, and will leave the remaining data on disk. The AssetBundle's Objects will be loaded on-demand as loading methods (e.g. *AssetBundle.Load*) are called or as their InstanceIDs are dereferenced. No excess memory will be consumed in this scenario. In the Unity Editor, the API will load the entire AssetBundle into memory, as if the bytes were read off disk and *AssetBundle.LoadFromMemoryAsync* was used. This API can cause memory spikes to appear during AssetBundle loading if the project is profiled in the Unity Editor. This should not affect performance on-device and these spikes should be re-tested on-device before taking remedial action.

*Note:* On Android devices with Unity 5.3 or older, this API will fail when trying to load AssetBundles from the Streaming Assets path. This issue has been resolved in Unity 5.4. For more details, see the section **Distribution - shipped with project** section of the **AssetBundle usage patterns** step.

*Prior to Unity 5.3, this API was known as **AssetBundle.CreateFromFile**. Its functionality has not been changed.*

#### 4.3.3. AssetBundleDownloadHandler

The [UnityWebRequest](http://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.html) API allows developers to specify exactly how Unity should handle downloaded data and allows developers to eliminate unnecessary memory usage. The simplest way to download an AssetBundle using UnityWebRequest is call [UnityWebRequest.GetAssetBundle](http://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.GetAssetBundle.html).

For the purposes of this guide, the class of interest is [DownloadHandlerAssetBundle](http://docs.unity3d.com/ScriptReference/Networking.DownloadHandlerAssetBundle.html). Using a worker thread, it streams downloaded data into a fixed-size buffer and then spools the buffered data to either temporary storage or the AssetBundle cache, depending on how the Download Handler has been configured. All of these operations occur in native code, eliminating the risk of expanding the managed heap. Additionally, this Download Handler does *not* keep a native-code copy of all downloaded bytes, further reducing the memory overhead of downloading an AssetBundle.

LZMA-compressed AssetBundles will be decompressed during download and cached using LZ4 compression. This behavior may be changed by setting [Caching.CompressionEnabled](https://docs.unity3d.com/ScriptReference/Caching-compressionEnabled.html).

When the download is complete, the [assetBundle](http://docs.unity3d.com/ScriptReference/Networking.DownloadHandlerAssetBundle-assetBundle.html) property of the Download Handler provides access to the downloaded AssetBundle, as if *AssetBundle.LoadFromFile* had been called on the downloaded AssetBundle.

If caching information is provided to a UnityWebRequest object, and the requested AssetBundle already exists in Unity's cache, then the AssetBundle will become available immediately and this API will operate identically to *AssetBundle.LoadFromFile*.

Prior to Unity 5.6, the UnityWebRequest system used a fixed pool of worker threads and an internal job system to safeguard against excessive, concurrent downloads. The size of the thread pool was not configurable. In Unity 5.6, these safeguards have been removed to accommodate more modern hardware, and allow for faster access to HTTP response codes and headers.

#### 4.3.4. WWW.LoadFromCacheOrDownload

*Note: Beginning in Unity 2017.1,* [***WWW.LoadFromCacheOrDownload***](http://docs.unity3d.com/ScriptReference/WWW.LoadFromCacheOrDownload.html) *simply wraps around UnityWebRequest. Accordingly, developers using Unity 2017.1 or higher should migrate to UnityWebRequest.* ***WWW.LoadFromCacheOrDownload*** *will be deprecated in a future release.*

*The following information is applicable to Unity 5.6 or older.*

[WWW.LoadFromCacheOrDownload](http://docs.unity3d.com/ScriptReference/WWW.LoadFromCacheOrDownload.html) is an API that allows loading of Objects from both remote servers and local storage. Files can be loaded from local storage via a file:// URL. If the AssetBundle is present in the Unity cache, this API will behave exactly like *AssetBundle.LoadFromFile*.

If an AssetBundle has not yet been cached, then *WWW.LoadFromCacheOrDownload* will read the AssetBundle from its source. If the AssetBundle is compressed, it will be decompressed using a worker thread and written into the cache. Otherwise, it will be written directly into the cache via the worker thread. Once the AssetBundle is cached, *WWW.LoadFromCacheOrDownload* will load header information from the cached, decompressed AssetBundle. The API will then behave identically to an AssetBundle loaded with *AssetBundle.LoadFromFile*. This cache is shared between *WWW.LoadFromCacheOrDownload* and *UnityWebRequest*. Any AssetBundle downloaded with one API will also be available via the other API.

While the data will be decompressed and written to the cache via a fixed-size buffer, the WWW object will keep a full copy of the AssetBundle's bytes in native memory. This extra copy of the AssetBundle is kept to support the [WWW.bytes](http://docs.unity3d.com/ScriptReference/WWW-bytes.html) property.

Due to the memory overhead of caching an AssetBundle's bytes in the WWW object, AssetBundles should remain small - a few megabytes, at most. For more discussion of AssetBundle sizing, see the[ ](https://unity3d.com/learn/tutorials/topics/best-practices/asset-bundle-usage-patterns#Asset_Assignment_Strategies)**Asset assignment strategies** section in the[ ](https://unity3d.com/learn/tutorials/topics/best-practices/asset-bundle-usage-patterns)**AssetBundle usage patterns** chapter.

Unlike UnityWebRequest, each call to this API will spawn a new worker thread. Accordingly, on platforms with limited memory, such as mobile devices, only a single AssetBundle at a time should be downloaded using this API, in order to avoid memory spikes. Be careful of creating an excessive number of threads when calling this API multiple times. If more than 5 AssetBundles need to be downloaded, create and manage a download queue in script code to ensure that only a few AssetBundle downloads are running occurring simultaneously.

#### 4.3.5. Recommendations

In general, AssetBundle.LoadFromFile should be used whenever possible. This API is the most efficient in terms of speed, disk usage and runtime memory usage.

For projects that must download or patch AssetBundles, it is strongly recommended to use[ ](https://unity3d.com/learn/tutorials/topics/best-practices/assetbundle-fundamentals?playlist=30089#AssetBundleDownloadHandler)UnityWebRequest for projects using Unity 5.3 or newer, and WWW.LoadFromCacheOrDownload for projects using Unity 5.2 or older. As detailed in the[ ](https://unity3d.com/learn/tutorials/topics/best-practices/asset-bundle-usage-patterns#Distribution)**Distribution** section, it is possible to prime the AssetBundle Cache with Bundles included within a project's installer.

When using either *UnityWebRequest* or *WWW.LoadFromCacheOrDownload*, ensure that the downloader code properly calls *Dispose* after loading the AssetBundle. Alternately, C#'s[ using](https://msdn.microsoft.com/en-us//library/yh598w02.aspx) statement is the most convenient way to ensure that a *WWW* or *UnityWebRequest* is safely disposed.

For projects with substantial engineering teams that require unique, specific caching or downloading requirements, a custom downloader may be considered. Writing a custom downloader is a non-trivial engineering task, and any custom downloader should be made compatible with *AssetBundle.LoadFromFile.* See the **Distribution** section of the next step for more details.

### 4.4. Loading Assets From AssetBundles

UnityEngine.Objects can be loaded from AssetBundles using three distinct APIs that are all attached to the *AssetBundle* object, which have both synchronous and asynchronous variants:

- [LoadAsset](https://docs.unity3d.com/ScriptReference/AssetBundle.LoadAsset.html) ([LoadAssetAsync](https://docs.unity3d.com/ScriptReference/AssetBundle.LoadAssetAsync.html))
- [LoadAllAssets](https://docs.unity3d.com/ScriptReference/AssetBundle.LoadAllAssets.html) ([LoadAllAssetsAsync](https://docs.unity3d.com/ScriptReference/AssetBundle.LoadAllAssetsAsync.html))
- [LoadAssetWithSubAssets](https://docs.unity3d.com/ScriptReference/AssetBundle.LoadAssetWithSubAssets.html) ([LoadAssetWithSubAssetsAsync](https://docs.unity3d.com/ScriptReference/AssetBundle.LoadAssetWithSubAssetsAsync.html))

The synchronous versions of these APIs will always be faster than their asynchronous counterpart, by at least one frame.

Asynchronous loads will load multiple Objects per frame, up to their time-slice limits. See the[ ](https://unity3d.com/learn/tutorials/topics/best-practices/assetbundle-fundamentals?playlist=30089#Lowlevel_Loading_Details)**Low-level loading details** section for the underlying technical reasons for this behavior.

*LoadAllAssets* should be used when loading multiple independent UnityEngine.Objects. It should only be used when the majority or all of the Objects within an AssetBundle need to be loaded. Compared to the other two APIs, *LoadAllAssets* is slightly faster than multiple individual calls to *LoadAssets*. Therefore, if the number of assets to be loaded is large, but less than 66% of the AssetBundle needs to be loaded at a single time, consider splitting the AssetBundle into multiple smaller bundles and using *LoadAllAssets*.

*LoadAssetWithSubAssets* should be used when loading a composite Asset which contains multiple embedded Objects, such as an FBX model with embedded animations or a sprite atlas with multiple sprites embedded inside it. If the Objects that need to be loaded all come from the same Asset, but are stored in an AssetBundle with many other unrelated Objects, then use this API.

For any other case, use *LoadAsset* or *LoadAssetAsync*.

#### 4.4.1. Low-level loading details

UnityEngine.Object loading is performed off the main thread: an Object's data is read from storage on a worker thread. Anything which does not touch thread-sensitive parts of the Unity system (scripting, graphics) will be converted on the worker thread. For example, VBOs will be created from meshes, textures will be decompressed, etc.

From Unity 5.3 onward, Object loading has been parallelized. Multiple Objects are deserialized, processed and integrated on worker threads. When an Object finishes loading, its *Awake* callback will be invoked and the Object will become available to the rest of the Unity Engine during the next frame.

The synchronous *AssetBundle.Load* methods will pause the main thread until Object loading is complete. They will also time-slice Object loading so that Object integration does not occupy more than a certain number of milliseconds of frame time. The number of milliseconds is set by the property *Application.backgroundLoadingPriority*:

- *ThreadPriority.High*: Maximum 50 milliseconds per frame
- *ThreadPriority.Normal*: Maximum 10 milliseconds per frame
- *ThreadPriority.BelowNormal*: Maximum 4 milliseconds per frame
- *ThreadPriority.Low*: Maximum 2 milliseconds per frame.

From Unity 5.2 onwards, multiple Objects are loaded until the frame-time limit for Object loading is reached. Assuming all other factors equal, the asynchronous variants of the asset loading APIs will always take longer to complete than the comparable synchronous version due to the minimum one-frame delay between issuing the asynchronous call and the object becoming available to the Engine.

#### 4.4.2. AssetBundle dependencies

The dependencies among AssetBundles are automatically tracked using two different APIs, depending on the runtime environment. In the Unity Editor, AssetBundle dependencies can be queried via the[ AssetDatabase](http://docs.unity3d.com/ScriptReference/AssetDatabase.html) API. AssetBundle assignments and dependencies can be accessed and changed via the[ AssetImporter](http://docs.unity3d.com/ScriptReference/AssetImporter.html) API. At runtime, Unity provides an optional API to load the dependency information generated during an AssetBundle build via a ScriptableObject-based[ AssetBundleManifest](http://docs.unity3d.com/ScriptReference/AssetBundleManifest.html) API.

An AssetBundle is dependent upon another AssetBundle when one or more of the parent AssetBundle's UnityEngine.Objects refers to one or more of the other AssetBundle's UnityEngine.Objects. For more information on inter-Object references, see the[ ](https://unity3d.com/learn/tutorials/temas/best-practices/assets-objects-and-serialization#InterObject_References)**Inter-Object references** section of the[ ](https://unity3d.com/learn/tutorials/temas/best-practices/assets-objects-and-serialization)**Assets, Objects and Serialization** step.

As described in the **Serialization and instances** section of that step, AssetBundles serve as sources for the source data identified by the FileGUID & LocalID of each Object contained within the AssetBundle.

Because an Object is loaded when its Instance ID is first dereferenced, and because an Object is assigned a valid Instance ID when its AssetBundle is loaded, the order in which AssetBundles are loaded is not important. Instead, it is important to load all AssetBundles that contain dependencies of an Object before loading the Object itself. Unity will not attempt to automatically load any child AssetBundles when a parent AssetBundle is loaded.

**Example:**

Assume *material A* refers to *texture B*. Material A is packaged into AssetBundle 1, and texture B is packaged into AssetBundle 2.



![img](https://connect-cdn-prd-cn.unitychina.cn/20190130/f2a6f87c-5842-4acb-b220-2d7917e1f29f_ab1.jpg)



In this use case, AssetBundle 2 must be loaded *prior* to loading Material A out of AssetBundle 1.

This does not imply that AssetBundle 2 must be loaded before AssetBundle 1, or that Texture B must be loaded explicitly from AssetBundle 2. It is sufficient to have AssetBundle 2 loaded prior to loading Material A out of AssetBundle 1.

However, Unity *will not* automatically load AssetBundle 2 when AssetBundle 1 is loaded. This must be done manually in script code.

For more information on AssetBundle dependencies, refer to the [manual page](https://docs.unity3d.com/Manual/AssetBundles-Dependencies.html).

#### 4.4.3. AssetBundle manifests

When executing the AssetBundle build pipeline using the *BuildPipeline.BuildAssetBundles* API, Unity serializes an Object containing each AssetBundle's dependency information. This data is stored in a separate AssetBundle, which contains a single Object of the[ AssetBundleManifest](http://docs.unity3d.com/ScriptReference/AssetBundleManifest.html) type.

This Asset will be stored in an AssetBundle with the same name as the parent directory where the AssetBundles are being built. If a project builds its AssetBundles to a folder at *(projectroot)/build/Client/*, then the AssetBundle containing the manifest will be saved as *(projectroot)/build/Client/Client.manifest*.

The AssetBundle containing the manifest can be loaded, cached and unloaded just like any other AssetBundle.

The AssetBundleManifest Object itself provides the[ GetAllAssetBundles](http://docs.unity3d.com/ScriptReference/AssetBundleManifest.GetAllAssetBundles.html) API to list all AssetBundles built concurrently with the manifest and two methods to query the dependencies of a specific AssetBundle:

- [AssetBundleManifest.GetAllDependencies](http://docs.unity3d.com/ScriptReference/AssetBundleManifest.GetAllDependencies.html) returns all of an AssetBundle's hierarchical dependencies, which includes the dependencies of the AssetBundle's direct children, its children's children, etc.
- [AssetBundleManifest.GetDirectDependencies](http://docs.unity3d.com/ScriptReference/AssetBundleManifest.GetDirectDependencies.html) returns only an AssetBundle's direct children

Note that both of these APIs allocate string arrays. Accordingly, they should only be used sparingly, and not during performance-sensitive portions of an application's lifetime.

#### 4.4.4. Recommendations

In many cases, it is preferable to load as many needed Objects as possible before players enter performance-critical areas of an application, such as the main game level or world. This is particularly critical on mobile platforms, where access to local storage is slow and the memory churn of loading and unloading Objects at play-time can trigger the garbage collector.

For projects that must load and unload Objects while the application is interactive, see the[ ](https://unity3d.com/learn/tutorials/topics/best-practices/asset-bundle-usage-patterns#Managing_Loaded_Assets)**Managing loaded assets** section of the **AssetBundle usage patterns** step for more information on unloading Objects and AssetBundles.

## 5. AssetBundle usage patterns

The previous step in this series covered the[ ](https://unity3d.com/learn/tutorials/topics/best-practices/asset-bundle-fundamentals)**fundamentals of AssetBundles,** which included the low-level behavior of various loading APIs. This chapter discusses problems and potential solutions to various aspects of using AssetBundles in practice.

## Managing loaded Assets

It is critical to carefully control the size and number of loaded Objects in memory-sensitive environments. Unity does not automatically unload Objects when they are removed from the active scene. Asset cleanup is triggered at specific times, and it can also be triggered manually.

AssetBundles themselves must be carefully managed. An AssetBundle backed by a file on local storage (either in the Unity cache or one loaded via [AssetBundle.LoadFromFile](http://docs.unity3d.com/ScriptReference/AssetBundle.LoadFromFile.html)) has minimal memory overhead, rarely consuming more than a few dozen kilobytes. However, this overhead can still become problematic if a large number of AssetBundles are present.

As most projects allow users to re-experience content (such as replaying a level), it's important to know when to load or unload an AssetBundle. ***If an AssetBundle is unloaded improperly, it can cause Object duplication in memory.*** Improperly unloading AssetBundles can also result in undesirable behavior in certain circumstances, such as causing textures to go missing. To understand why this can happen, refer to the **Inter-Object references** section of the **Assets, Objects, and serialization** step.

The most important thing to understand when managing assets and AssetBundles is the difference in behavior when calling [AssetBundle.Unload](http://docs.unity3d.com/ScriptReference/AssetBundle.Unload.html) with either true or false for the *unloadAllLoadedObjects* parameter.

This API will unload the header information of the AssetBundle being called. The *unloadAllLoadedObjects* parameter determines whether to also unload all Objects instantiated from this AssetBundle. If set to *true*, then all Objects originating from the AssetBundle will also be immediately unloaded – even if they are currently being used in the active scene.

For example, assume a material *M* was loaded from an AssetBundle *AB*, and assume *M* is currently in the active scene.

![img](https://connect-cdn-prd-cn.unitychina.cn/20190130/76e89c67-741d-4b74-9356-fd72c187f4dc_ab2a.jpg)

If *AssetBundle.Unload(true)* is called, then *M* will be removed from the scene, destroyed and unloaded. However, if *AssetBundle.Unload(false)* is called, then *AB*'s header information will be unloaded but *M* will remain in the scene and will still be functional. Calling *AssetBundle.Unload(false)* breaks the link between *M* and *AB*. If *AB* is loaded again later, then fresh copies of the Objects contained in *AB* will be loaded into memory.

![img](https://connect-cdn-prd-cn.unitychina.cn/20190130/f82652fb-5175-4b25-b362-bfdd8527a340_ab2b.jpg)

If *AB* is loaded again later, then a new copy of the AssetBundle's header information will be reloaded. However, *M* was not loaded from this new copy of *AB*. Unity does not establish any link between the new copy of *AB* and *M*.

![img](https://connect-cdn-prd-cn.unitychina.cn/20190130/9bad77ee-6cdd-4e5c-886d-dc02142e421d_ab2c.jpg)

If *AssetBundle.LoadAsset()* were called to reload *M*, Unity would not interpret the old copy of *M* as being an instance of the data in *AB*. Therefore, Unity will load a new copy of *M* and there will be **two** identical copies of *M* in the scene.

![img](https://connect-cdn-prd-cn.unitychina.cn/20190130/e1119799-f21d-4d2f-a56a-550302752728_ab2d.jpg)

For most projects, this behavior is undesirable. Most projects should use *AssetBundle.Unload(true)* and adopt a method to ensure that Objects are not duplicated. Two common methods are:

1. Having well-defined points during the application's lifetime at which transient AssetBundles are unloaded, such as between levels or during a loading screen. This is the simpler and most common option.
2. Maintaining reference-counts for individual Objects and unload AssetBundles only when all of their constituent Objects are unused. This permits an application to unload and reload individual Objects without duplicating memory.

If an application must use *AssetBundle.Unload(false)*, then individual Objects can only be unloaded in two ways:

1. Eliminate all references to an unwanted Object, both in the scene and in code. After this is done, call [Resources.UnloadUnusedAssets](http://docs.unity3d.com/ScriptReference/Resources.UnloadUnusedAssets.html).
2. Load a scene non-additively. This will destroy all Objects in the current scene and invoke [Resources.UnloadUnusedAssets](http://docs.unity3d.com/ScriptReference/Resources.UnloadUnusedAssets.html) automatically.

If a project has well-defined points where the user can be made to wait for Objects to load and unload, such as in between game modes or levels, these points should be used to unload as many Objects as necessary and to load new Objects.

The simplest way to do this is to package discrete chunks of a project into scenes, and then build those scenes into AssetBundles, along with all of their dependencies. The application can then enter a "loading" scene, fully unload the AssetBundle containing the old scene, and then load the AssetBundle containing the new scene.

While this is the simplest flow, some projects require more complex AssetBundle management. As every project is different, there is no universal AssetBundle design pattern.

When deciding how to group Objects into AssetBundles, it is generally best to start by bundling Objects into AssetBundles if they must be loaded or updated at the same time. For example, consider a role-playing game. Individual maps and cutscenes can be grouped into AssetBundles by scene, but some Objects will be needed in most scenes. AssetBundles could be built to provide portraits, the in-game UI, and different character models and textures. These latter Objects and Assets could then be grouped into a second set of AssetBundles that are loaded at startup and remain loaded for the lifetime of the app.

Another problem can arise if Unity must reload an Object from its AssetBundle after the AssetBundle has been unloaded. In this case, the reload will fail and the Object will appear in the Unity Editor's hierarchy as a (Missing) Object.

This primarily occurs when Unity loses and regains control over its graphics context, such as when a mobile app is suspended or the user locks their PC. In this case, Unity must re-upload textures and shaders to the GPU. If the source AssetBundle for these assets is unavailable, the application will render Objects in the scene as magenta.

### 5.2. Distribution

There are two basic ways to distribute a project's AssetBundles to clients: installing them simultaneously with the project or downloading them after installation.

The decision whether to ship AssetBundles within or after installation is driven by the capabilities and restrictions of the platforms on which the project will run. Mobile projects usually opt for post-install downloads to reduce initial install size and remain below over-the-air download size limits. Console and PC projects generally ship AssetBundles with their initial install.

Proper architecture permits patching new or revised content into your project post-install regardless of how the AssetBundles are delivered initially. For more information on this, see the [Patching with AssetBundles](https://docs.unity3d.com/Manual/AssetBundles-Patching.html) section of the Unity Manual.

#### 5.2.1. Shipped with project

Shipping AssetBundles with the project is the simplest way to distribute them as it does not require additional download-management code. There are two major reasons why a project might include AssetBundles with the install:

- To reduce project build times and permit simpler iterative development. If these AssetBundles do not need to be updated separately from the application itself, then the AssetBundles can be included with the application by storing the AssetBundles in Streaming Assets. See the **Streaming Assets** section, below.
- To ship an initial revision of updatable content. This is commonly done to save end-users time after their initial install or to serve as the basis for later patching. Streaming Assets is not ideal for this case. However, if writing a custom downloading and caching system is not an option, then an initial revision of updatable content can be loaded into the Unity cache from Streaming Assets (See the **Cache Priming** section, below).

##### 5.2.1.1. Streaming Assets

The easiest way to include any type of content, including AssetBundles, within a Unity application at install time is to build the content into the */Assets/StreamingAssets/* folder, prior to building the project. Anything contained in the *StreamingAssets* folder at build time will be copied into the final application.

The full path to the *StreamingAssets* folder on local storage is accessible via the property [Application.streamingAssetsPath](http://docs.unity3d.com/ScriptReference/Application-streamingAssetsPath.html) at runtime. The AssetBundles can then be loaded with via *AssetBundle.LoadFromFile* on most platforms.

***Android Developers:*** On Android, assets in the StreamingAssets folders are stored into the APK and may take more time to load if they are compressed, as files stored in an APK can use different storage algorithms. The algorithm used may vary from one Unity version to another. You can use an archiver such as 7-zip to open the APK to determine if the files are compressed or not. If they are, you can expect AssetBundle.LoadFromFile() to perform more slowly. In this case, you can retrieve the cached version by using [UnityWebRequest.GetAssetBundle](https://docs.unity3d.com/ScriptReference/Networking.UnityWebRequest.GetAssetBundle.html) as a workaround. By using UnityWebRequest, the AssetBundle will be uncompressed and cached during the first run, allowing for following executions to be faster. Note that this will will take more storage space, as the AssetBundle will be copied to the cache. Alternatively, you can export your Gradle project and add an extension to your AssetBundles at build time. You can then edit the build.gradle file and add that extension to the noCompress section. Once done, you should be able to use AssetBundle.LoadFromFile() without having to pay the decompression performance cost.

*Note:* Streaming Assets is not a writable location on some platforms. If a project's AssetBundles need to be updated after installation, either use *WWW.LoadFromCacheOrDownload* or write a custom downloader.

#### 5.2.2. Downloaded post-install

The favored method of delivering AssetBundles to mobile devices is to download them after app installation. This also allows the content to be updated after installation without forcing users to re-download the entire application. On many platforms, application binaries must undergo an expensive and lengthy re-certification process. Therefore, developing a good system for post-install downloads is vital.

The simplest way to deliver AssetBundles is to place them on a web server and deliver them via *UnityWebRequest*. Unity will automatically cache downloaded AssetBundles on local storage. If the downloaded AssetBundle is LZMA compressed, the AssetBundle will be stored in the cache either as uncompressed or re-compressed as LZ4 (dependent on the [Caching.compressionEnabled](https://docs.unity3d.com/ScriptReference/Caching-compressionEnabled.html) setting), for faster loading in the future. If the downloaded bundle is LZ4 compressed, the AssetBundle will be stored compressed. If the cache fills up, Unity will remove the least recently used AssetBundle from the cache. See the **Built-in caching** section for more details.

It is generally recommended to start by using *UnityWebRequest* when possible, or *WWW.LoadFromCacheOrDownload* only if using Unity 5.2 or older. Only invest in a custom download system if the built-in APIs' memory consumption, caching behavior or performance are unacceptable for a specific project, or if a project must run platform-specific code to achieve its requirements.

Examples of situations which may prevent the use of *UnityWebRequest* or *WWW.LoadFromCacheOrDownload*:

- When fine-grained control over the AssetBundle cache is required
- When a project needs to implement a custom compression strategy
- When a project wishes to use platform-specific APIs to satisfy certain requirements, such as the need to stream data while inactive.
- *Example:* Using iOS' Background Tasks API to download data while in the background.
- When AssetBundles must be delivered over SSL on platforms where Unity does not have proper SSL support (such as PC).

#### 5.2.3. Built-in caching

Unity has a built-in AssetBundle caching system that can be used to cache AssetBundles downloaded via the *UnityWebRequest* API, which has an overload accepting an AssetBundle version number as an argument. This number is *not* stored inside the AssetBundle, and is *not* generated by the AssetBundle system.

The caching system keeps track of the last version number passed to *UnityWebRequest*. When this API is called with a version number, the caching system checks to see if there is a cached AssetBundle by comparing version numbers. If these numbers match, the system will load the cached AssetBundle. If the numbers do not match, or there is no cached AssetBundle, then Unity will download a new copy. This new copy will be associated with the new version number.

***AssetBundles in the caching system are identified only by their file names***, and not by the full URL from which they are downloaded. This means that an AssetBundle with the same file name can be stored in multiple different locations, such as a Content Delivery Network. As long as the file names are identical, the caching system will recognize them as the same AssetBundle.

It is up to each individual application to determine an appropriate strategy for assigning version numbers to AssetBundles, and to pass these numbers to *UnityWebRequest*. The numbers may come from a unique identifiers of sorts, such as a CRC value. Note that while AssetBundleManifest.GetAssetBundleHash() may also be used for this purpose, we don’t recommend this function for versioning, as it provides just an estimation, and not a true hash calculation).

See the [Patching with AssetBundles](https://docs.unity3d.com/Manual/AssetBundles-Patching.html) section of the Unity Manual for more details.

In Unity 2017.1 onward, the [Caching](https://docs.unity3d.com/ScriptReference/Caching.html) API has been extended to provide more granular control, by allow developers to select an active cache from multiple caches. Prior versions of Unity may only modify [Caching.expirationDelay](https://docs.unity3d.com/560/Documentation/ScriptReference/Caching-expirationDelay.html) and [Caching.maximumAvailableDiskSpace](https://docs.unity3d.com/560/Documentation/ScriptReference/Caching-maximumAvailableDiskSpace.html) to remove cached items (these properties remain in Unity 2017.1 in the [Cache class](https://docs.unity3d.com/ScriptReference/Cache.html)).

[expirationDelay](http://docs.unity3d.com/ScriptReference/Caching-expirationDelay.html) is the minimum number of seconds that must elapse before an AssetBundle is automatically deleted. If an AssetBundle is not accessed during this time, it will be deleted automatically.

[maximumAvailableDiskSpace](http://docs.unity3d.com/ScriptReference/Caching-maximumAvailableDiskSpace.html) specifies the amount of space on local storage, in bytes, that the cache may use before it begins deleting AssetBundles that have been used less recently than the *expirationDelay*. When the limit is reached, Unity will delete the AssetBundle in the cache which was least recently opened (or marked as used via *Caching.MarkAsUsed*). Unity will delete cached AssetBundles until there is sufficient space to complete the new download.

##### 5.2.3.1. Cache Priming

Because AssetBundles are identified by their file names, it is possible to "prime" the cache with AssetBundles shipped with the application. To do this, store the initial or base version of each AssetBundle in */Assets/StreamingAssets/*. The process is identical to the one detailed in the[ ](https://unity3d.com/learn/tutorials/temas/best-practices/assetbundle-usage-patterns?playlist=30089#Shipped_with_Project)**Shipped with project** section.

The cache can be populated by loading AssetBundles from *Application.streamingAssetsPath* the first time the application is run. From then on, the application can call *UnityWebRequest* normally (UnityWebRequest can also be used to initially load AssetBundles from the StreamingAssets path as well).

#### 5.2.3. Custom downloaders

Writing a custom downloader gives an application full control over how AssetBundles are downloaded, decompressed and stored. As the engineering work involved is non-trivial, we recommend this approach only for larger teams. There are four major considerations when writing a custom downloader:

- Download mechanism
- Storage location
- Compression type
- Patching

For information on patching AssetBundles, see the[ Patching with AssetBundles](https://docs.unity3d.com/Manual/AssetBundles-Patching.html) section of the Unity Manual.

##### 5.2.3.1. Downloading

For most applications, HTTP is the simplest method to download AssetBundles. However, implementing an HTTP-based downloader is not the simplest task. Custom downloaders must avoid excessive memory allocations, excessive thread usage and excessive thread wakeups. Unity's WWW class is unsuitable for reasons exhaustively described in the **WWW.LoadFromCacheOrDownload** section of the **AssetBundle fundamentals** step.

When writing a custom downloader, there are three options:

- C#'s HttpWebRequest and WebClient classes
- Custom native plugins
- Asset store packages

###### 5.2.3.1.1. C# classes

If an application does not require HTTPS/SSL support, C#'s [WebClient](https://msdn.microsoft.com/en-us/library/system.net.webclient(v=vs.110).aspx) class provides the simplest possible mechanism for downloading AssetBundles. It is capable of asynchronously downloading any file directly to local storage without excessive managed memory allocation.

To download an AssetBundle with WebClient, allocate an instance of the class and pass it the URL of the AssetBundle to download and a destination path. If more control is required over the request's parameters, it is possible to write a downloader using C#'s [HttpWebRequest](https://msdn.microsoft.com/en-us/library/system.net.httpwebrequest(v=vs.90).aspx) class:

1. Get a byte stream from *HttpWebResponse.GetResponseStream*.
2. Allocate a fixed-size byte buffer on the stack.
3. Read from the response stream into the buffer.
4. Write the buffer to disk using C#'s File.IO APIs, or any other streaming IO system.

###### 5.2.3.1.2. Asset Store Packages

Several asset store packages offer native-code implementations to download files via HTTP, HTTPS and other protocols. Before writing a custom native-code plugin for Unity, it is recommended to evaluate available Asset Store packages.

###### 5.2.3.1.3. Custom Native Plugins

Writing a custom native plugin is the most time-intensive, but most flexible method for downloading data in Unity. Due to the high programming time requirements and high technical risk, this method is only recommended if no other method is capable of satisfying an application's requirements. For example, a custom native plugin may be necessary if an application must use SSL communication on platforms without C# SSL support in Unity.

A custom native plugin will generally wrap a target platform's native downloading APIs. Examples include [NSURLConnection](https://developer.apple.com/library/ios/documentation/Cocoa/Reference/Foundation/Classes/NSURLConnection_Class/) on iOS and [java.net.HttpURLConnection](http://download.java.net/jdk7/archive/b123/docs/api/java/net/HttpURLConnection.html) on Android. Consult each platform's native documentation for further details on using these APIs.

##### 5.2.3.2. Storage

On all platforms, *Application.persistentDataPath* points to a writable location that should be used for storing data that should persist between runs of an application. When writing a custom downloader, it is strongly recommended to use a subdirectory of *Application.persistentDataPath* to store downloaded data.

*Application.streamingAssetPath* is not writable and is a poor choice for an AssetBundle cache. Example locations for *streamingAssetsPath* include:

- **OSX**: Within .app package; not writable.
- **Windows**: Within install directory (e.g. *Program Files*); usually not writable
- **iOS**: Within .ipa package; not writable
- **Android**: Within .apk file; not writable

### 5.3. Asset Assignment Strategies

Deciding how to divide a project's assets into AssetBundles is not simple. It is tempting to adopt a simplistic strategy, such as placing all Objects in their own AssetBundle or using only a single AssetBundle, but these solutions have significant drawbacks:

- Having too few AssetBundles...
- Increases runtime memory usage
- Increases loading times
- Requires larger downloads
- Having too many AssetBundles...
- Increases build times
- Can complicate development
- Increases total download time

The key decision is how to group Objects into AssetBundles. The primary strategies are:

- Logical entities
- Object Types
- Concurrent content

More information about these grouping strategies can be found in the [Manual](https://docs.unity3d.com/Manual/AssetBundles-Preparing.html).

### 5.4. Common pitfalls

This section describes several problems that commonly appear in projects using AssetBundles.

#### 5.5.1. Asset duplication

Unity 5's AssetBundle system will discover all dependencies of an Object when the Object is built into an AssetBundle. This dependency information is used to determine the set of Objects that will be included in an AssetBundle.

Objects that are explicitly assigned to an AssetBundle will only be built into that AssetBundle. An Object is "explicitly assigned" when that Object's AssetImporter has its *assetBundleName* property set to a non-empty string. This can be done in the Unity Editor by selecting an AssetBundle in the Object's Inspector, or from Editor scripts.

Objects can also be assigned to an AssetBundle by defining them as part of an [AssetBundle building map](https://docs.unity3d.com/ScriptReference/AssetBundleBuild.html), which is to be used in conjunction with the overloaded [BuildPipeline.BuildAssetBundles()](https://docs.unity3d.com/ScriptReference/BuildPipeline.BuildAssetBundles.html) function that takes in an array of AssetBundleBuild.

***Any Object that is not explicitly assigned in an AssetBundle will be included in all AssetBundles that contain 1 or more Objects that reference the untagged Object.***

For example, if two different Objects are assigned to two different AssetBundles, but both have references to a common dependency Object, then that dependency Object will be copied into *both* AssetBundles. The duplicated dependency will also be instanced, meaning that the two copies of the dependency Object will be considered different Objects with a different identifiers. This will increase the total size of the application's AssetBundles. This will also cause two different copies of the Object to be loaded into memory if the application loads both of its parents.

There are several ways to address this problem:

1. Ensure that Objects built into different AssetBundles do not share dependencies. Any Objects which do share dependencies can be placed into the same AssetBundle without duplicating their dependencies.

- This method usually is not viable for projects with many shared dependencies. It produces monolithic AssetBundles that must be rebuilt and re-downloaded too frequently to be convenient or efficient.

1. Segment AssetBundles so that no two AssetBundles that share a dependency will be loaded at the same time.

- This method may work for certain types of projects, such as level-based games. However, it still unnecessarily increases the size of the project's AssetBundles, and increases both build times and loading times.

1. Ensure that all dependency assets are built into their own AssetBundles. This entirely eliminates the risk of duplicated assets, but also introduces complexity. The application must track dependencies between AssetBundles, and ensure that the right AssetBundles are loaded before calling any *AssetBundle.LoadAsset* APIs.

Object dependencies are tracked via the *AssetDatabase* API, located in the *UnityEditor* namespace. As the namespace implies, this API is only available in the Unity Editor and not at runtime. *AssetDatabase.GetDependencies* can be used to locate all of the immediate dependencies of a specific Object or Asset. Note that these dependencies may have their own dependencies. Additionally, the *AssetImporter* API can be used to query the AssetBundle to which any specific Object is assigned.

By combining the *AssetDatabase* and *AssetImporter* APIs, it is possible to write an Editor script that ensures that all of an AssetBundle's direct or indirect dependencies are assigned to AssetBundles, or that no two AssetBundles share dependencies that have not been assigned to an AssetBundle. Due to the memory cost of duplicating assets, it is recommended that all projects have such a script.

#### 5.5.2. Sprite atlas duplication

Any automatically-generated sprite atlas will be assigned to the AssetBundle containing the Sprite Objects from which the sprite atlas was generated. If the sprite Objects are assigned to multiple AssetBundles, then the sprite atlas will not be assigned to an AssetBundle and will be duplicated. If the Sprite Objects are not assigned to an AssetBundle, then the sprite atlas will also not be assigned to an AssetBundle.

To ensure that sprite atlases are not duplicated, check that all sprites tagged into the same sprite atlas are assigned to the same AssetBundle.

Note that in Unity 5.2.2p3 and older, automatically-generated sprite atlases will never be assigned to an AssetBundle. Because of this, they will be included in any AssetBundles containing their constituent sprites and also any AssetBundles referencing their constituent sprites. Because of this problem, it is strongly recommended that all Unity 5 projects using Unity's sprite packer upgrade to Unity 5.2.2p4, 5.3 or any newer version of Unity.

#### 5.5.3. Android textures

Due to heavy device fragmentation in the Android ecosystem, it is often necessary to compress textures into several different formats. While all Android devices support ETC1, ETC1 does not support textures with alpha channels. Should an application not require OpenGL ES 2 support, the cleanest way to solve the problem is to use ETC2, which is supported by all Android OpenGL ES 3 devices.

Most applications need to ship on older devices where ETC2 support is unavailable. One way to solve this problem is with Unity 5's AssetBundle Variants (refer to Unity's Android optimization guide for details on other options).

To use AssetBundle Variants, all textures that cannot be cleanly compressed using ETC1 must be isolated into texture-only AssetBundles. Next, create sufficient variants of these AssetBundles to support the non-ETC2-capable slices of the Android ecosystem, using vendor-specific texture compression formats such as DXT5, PVRTC and ATITC. For each AssetBundle Variant, change the included textures' TextureImporter settings to the compression format appropriate to the Variant.

At runtime, support for the different texture compression formats can be detected using the[ SystemInfo.SupportsTextureFormat](http://docs.unity3d.com/ScriptReference/SystemInfo.SupportsTextureFormat.html) API. This information should be used to select and load the AssetBundle Variant containing textures compressed in a supported format.

More information on Android texture compression formats can be found[ here](http://developer.android.com/guide/topics/graphics/opengl.html#textures).

#### 5.5.4. iOS file handle overuse

***Current versions of Unity are not affected by this issue.***

In versions prior to Unity 5.3.2p2, Unity would hold an open file handle to an AssetBundle the entire time that the AssetBundle is loaded. This is not a problem on most platforms. However, iOS limits the number of file handles a process may simultaneously have open to 255. If loading an AssetBundle causes this limit to be exceeded, the loading call will fail with a "Too Many Open File Handles" error.

This was a common problem for projects trying to divide their content across many hundreds or thousands of AssetBundles.

For projects unable to upgrade to a patched version of Unity, temporary solutions are:

- Reducing the number of AssetBundles in use by merging related AssetBundles
- Using *AssetBundle.Unload(false)* to close an AssetBundle's file handle, and managing the loaded Objects' lifecycles manually

### 5.5. AssetBundle Variants

A key feature of the AssetBundle system is the introduction of AssetBundle Variants. The purpose of Variants is to allow an application to adjust its content to better suit its runtime environment. Variants permit different UnityEngine.Objects in different AssetBundle files to appear as being the "same" Object when loading Objects and resolving Instance ID references. Conceptually, it permits two UnityEngine.Objects to appear to share the same File GUID & Local ID, and identifies the actual UnityEngine.Object to load by a string Variant ID.

There are two primary use cases for this system:

1. Variants simplify the loading of AssetBundles appropriate for a given platform.

- Example: A build system might create an AssetBundle containing high-resolution textures and complex shaders suitable for a standalone DirectX11 Windows build, and a second AssetBundle with lower-fidelity content intended for Android. At runtime, the project's resource loading code can then load the appropriate AssetBundle Variant for its platform, and the Object names passed into the AssetBundle.Load API do not need to change.

1. Variants allow an application to load different content on the same platform, but with different hardware.

- This is key for supporting a wide range of mobile devices. An iPhone 4 is incapable of displaying the same fidelity of content as the latest iPhone in any real-world application.
- On Android, AssetBundle Variants can be used to tackle the immense fragmentation of screen aspect ratios and DPIs between devices.

#### 5.5.1. Limitations

A key limitation of the AssetBundle Variant system is that it requires Variants to be built from distinct Assets. This limitation applies even if the only variations between those Assets is their import settings. If the only distinction between a texture built into Variant A and Variant B is the specific texture compression algorithm selected in the Unity texture importer, Variant A and Variant B must still be entirely different Assets. This means that Variant A and Variant B must be separate files on disk.

This limitation complicates the management of large projects as multiple copies of a specific Asset must be kept in source control. All copies of an Asset must be updated when developers wish to change the content of the Asset. There are no built-in workarounds for this problem.

Most teams implement their own form of AssetBundle Variants. This is done by building AssetBundles with well-defined suffixes appended to their filenames, in order to identify the specific variant a given AssetBundle represents. Custom code programmatically alters the importer settings of the included Assets when building these AssetBundles. Some developers have extended their custom systems to also be able to alter parameters on components attached to prefabs.

### 5.6. Compressed or uncompressed?

Whether to compress AssetBundles requires several important considerations, which include:

- **Loading time**: Uncompressed AssetBundles are much faster to load than compressed AssetBundles when loading from local storage or a local cache.
- **Build time**: LZMA and LZ4 are very slow when compressing files, and the Unity Editor processes AssetBundles serially. Projects with a large number of AssetBundles will spend a lot of time compressing them.
- **Application size**: If the AssetBundles are shipped in the application, compressing them will reduce the application's total size. Alternatively, the AssetBundles can be downloaded post-install.
- **Memory usage**: Prior to Unity 5.3, all of Unity's decompression mechanisms required the entire compressed AssetBundle to be loaded into memory prior to decompression. If memory usage is important, use either uncompressed or LZ4 compressed AssetBundles.
- **Download time**: Compression may only be necessary if the AssetBundles are large, or if users are in a bandwidth-constrained environment, such as downloading on low-speed or metered connections. If only a few tens of megabytes of data are being delivered to PCs on high-speed connections, it may be possible to omit compression.

#### 5.6.1. Crunch Compression

Bundles which consist primarily of DXT-compressed textures which use the Crunch compression algorithm should be built uncompressed.

### 5.7. AssetBundles and WebGL

All AssetBundle decompression and loading in a WebGL project must occur on the main thread, due to Unity’s WebGL export option not currently supporting worker threads. The downloading of AssetBundles is delegated to the browser using XMLHttpRequest. Once downloaded, compressed AssetBundles will be decompressed on Unity’s main thread, therefore stalling execution of the Unity content depending on the size of the bundle.

Unity recommends that developers prefer small asset bundles to avoid incurring performance issues. This approach will also be more memory efficient than using large asset bundles. Unity WebGL only supports LZ4-compressed and uncompressed asset bundles, however, it is possible to apply gzip/brotli compression on the bundles generated by Unity. In that case you will need to configure your web server accordingly so that the files are decompressed on download by the browser. See [here](https://docs.unity3d.com/Manual/webgl-deploying.html) for more details.

If you are using Unity 5.5 or older, consider avoiding LZMA for your AssetBundles and compress using LZ4 instead, which is decompressed very efficiently on-demand. Unity 5.6 removes LZMA as a compression option for the WebGL platform.