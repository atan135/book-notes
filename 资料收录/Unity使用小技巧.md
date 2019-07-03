1. 使用script在运行时给gameobject添加boxcollider并正确赋值大小

```c#
boxCol = gameObject.AddComponent<BoxCollider>();
Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
Renderer thisRenderer = transform.GetComponet<Renderer>();
bounds.Encapsulate(thisRenderer.bounds);
boxCol.center = bounds.center - transform.position;
boxCol.size = bounds.size;
```

