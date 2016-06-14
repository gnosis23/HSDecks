using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace HSDecks {
    public class FileStuff {
        public async static Task WriteToFileAsync(string str) {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile;
            try {
                sampleFile = await storageFolder.GetFileAsync("sample.txt");
            } catch (FileNotFoundException) {
                sampleFile = await storageFolder.CreateFileAsync("sample.txt", 
                    CreationCollisionOption.ReplaceExisting);
            }

            await FileIO.WriteTextAsync(sampleFile, str);
        }

        public async static Task<string> ReadFromFileAsync() {
            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            StorageFile sampleFile;
            try {
                sampleFile = await storageFolder.GetFileAsync("sample.txt");
            } catch (FileNotFoundException) {
                return "";
            }

            return await FileIO.ReadTextAsync(sampleFile);
        }
    }
}
