# HttpTriggeFileShare
HttpTrigger used to check Fileshare connectivity - Good for checking VNET and Private Endpoints on a fileshare are configured correctly


### Configuration -> Application settings:
```
FILE_SHARE = myfileshare // The name of the fileshare you want to connect to. Example: myfileshare

FOLDER_PATH = myfolder/mysubfolder // The path to the subfolder you want to look at. Example: myfolder/mysubfolder

CON_STR = DefaultEndpointsProtocol=https;AccountName={NameOfTheStorageAccount};AccountKey={MyAccountKey};EndpointSuffix=core.windows.net // The Connection String from Storage Account you want to access. Found under Storage Account -> Access Keys -> Connection String 
```
