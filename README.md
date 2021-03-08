# ChallegeVoolkia


## To Run on Linux:

### 1. Download .NET Core SDK 3.1
input this commands into a terminal: 
```
wget https://packages.microsoft.com/config/ubuntu/20.10/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb

```

then 
```
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1

```
Verify that the instalation was correct by runing:  
```
dotnet
```
### 2 Download the file as  .Zip
### 3 Open a terminal on the decompressed folder where the challengeVoolkia.csproj i'ts located
### 4 Write the next commands into the terminal:
```
dotnet build
```
```
dotnet run
```

This steps can be replicated in any distro that supports netcore 3.1 

## To run on Windows: 

### 1. Download the .Net Core sdk 3.1
go into https://dotnet.microsoft.com/download/dotnet/3.1 and select your operating system, download and install it.

### 2. Download the code as .zip
from this github page and decompress it using your favorite decompressing tool.

### 3. Download Visual Studio Code if you dont have it. 

### 4. Open visual studio code where the file ChallengeVoolkia.csproj is located

### 5. Open a new terminal and run the next commands:
```
dotnet build
```
wait until it finishes compiling and then:
```
dotnet run
```
## How to use it: 
To use it you can simply put a seller_id or varius seller id's separed by commas and then press enter. Do not touch any key into the console until it says finnished (seller_id)</br>
If you put several seller_ids wait until it displays that all of the files had been wrote. 
When it finishes writing the files, you can simply close the console and then go to the next section of this readme to know where the log files are located.

## Where are located the log files: 
The log files will be located where the .csproj file is. This files have a nomenclature of LOG-#Sellerd_id-.txt

## Aditional information for Voolkia: 
The requested log file with the output of the script its called <strong>LOG-179571326 -.txt</strong> and its located in the same folder as the csproj file.
