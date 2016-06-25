using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace HSDecks.Common {
    public class FileHelper {
        public static async Task<bool> IfFolderExistsAsync(StorageFolder storageFolder, string subFolderName) {
            try {
                IStorageItem item = await storageFolder.GetItemAsync(subFolderName);
                return (item != null);
            } catch {
                // Should never get here
                return false;
            }
        }

        public static async Task<StorageFolder> 
        GetFolderNotNullAsync(StorageFolder storageFolder, string subFolderName) {
            bool isSubFolderExist = await IfFolderExistsAsync(storageFolder, subFolderName);

            if (!isSubFolderExist) {
                // Create the sub folder.
                return await storageFolder.CreateFolderAsync(subFolderName,
                    CreationCollisionOption.ReplaceExisting);
            } else {
                // Just get the folder.
                return await storageFolder.GetFolderAsync(subFolderName);
            }
        }
    }
}
