using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainSoftLib.Compress
{
    public class MethodsCompress
    {
        private byte[] Compress(byte[] raw)
        {
            using (var mem = new MemoryStream())
            {
                using (var gzip = new GZipStream(mem, CompressionMode.Compress, true))
                {
                    gzip.Write(raw, 0, raw.Length);
                }

                return mem.ToArray();
            }
        }

        private byte[] Decompress(byte[] compressed)
        {
            const int Size = 4096;

            using (var gzip = new GZipStream(new MemoryStream(compressed), CompressionMode.Decompress))
            {
                var buffer = new byte[Size];

                using (var mem = new MemoryStream())
                {
                    int count = 0;

                    do
                    {
                        count = gzip.Read(buffer, 0, Size);

                        if (count > 0)
                        {
                            mem.Write(buffer, 0, Size);
                        }

                    } while (count > 0);

                    return mem.ToArray();
                }
            }
        }


        /// <summary>
        /// Sirve para comprimir un archivo a .gz
        /// </summary>
        /// <param name="fileToCompress">Ruta de archivo a comprimir</param>
        /// <param name="fileNameToCompressing">Nombre de archivo comprimido</param>
        /// <returns>true-false</returns>
        public bool Compress(FileInfo fileToCompress, FileInfo fileNameToCompressing)
        {
            bool Status = false;

            using (FileStream originalFileStream = fileToCompress.OpenRead())
            {
                if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                {
                    using (FileStream compressedFileStream = File.Create(fileNameToCompressing.FullName))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressionStream);
                            Status = true;
                        }
                    }
                }
            }
            return Status;
        }

        /// <summary>
        /// Sirve para descomprimir un archivo .gz
        /// </summary>
        /// <param name="fileToDecompress">Ruta de archivo a descomprimir</param>
        /// <returns>ruta de archivo descomprimido.</returns>
        public bool Decompress(FileInfo fileToDecompress, FileInfo fileNameToDecompressing)
        {
            bool Status = false;

            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = fileNameToDecompressing.FullName;

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Status = true;
                    }
                }
            }
            return Status;
        }


        public string Decompress(FileInfo fileToDecompress)
        {
            string Ruta_Full = "";

            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        Ruta_Full = newFileName;
                    }
                }
            }
            return Ruta_Full;
        }

        public DataSet ConvertCompressedToDataSet(byte[] data)
        {
            var binData = Decompress(data);
            using (var stream = new System.IO.MemoryStream(binData))
            {
                var bin = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return (DataSet)bin.Deserialize(stream);
            }
        }
    }
}
