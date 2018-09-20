import datetime
import os

for afile in os.listdir(os.getcwd()):
    if afile.endswith("arg"):
        print("arg file " + afile)
        os.rename(os.getcwd() + "\\" + afile, os.getcwd() + "\\" + "bbb" + afile)
        
