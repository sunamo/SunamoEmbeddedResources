namespace SunamoEmbeddedResources._sunamo;

/// <summary>
/// File system helper class for stream operations.
/// EN: Provides utility methods for working with streams and converting them to byte arrays.
/// CZ: Poskytuje pomocné metody pro práci se streamy a jejich konverzi na byte pole.
/// </summary>
internal class FS
{
    /// <summary>
    /// Converts a stream to a byte array.
    /// EN: Reads the entire stream content and returns it as a byte array, preserving the original stream position.
    /// CZ: Přečte celý obsah streamu a vrátí ho jako byte pole, zachová původní pozici streamu.
    /// </summary>
    /// <param name="stream">The stream to convert to byte array</param>
    /// <returns>The byte array containing all stream data</returns>
    internal static byte[] StreamToArrayBytes(System.IO.Stream stream)
    {
        if (stream == null)
        {
            return new byte[0];
        }

        long originalPosition = 0;

        if (stream.CanSeek)
        {
            originalPosition = stream.Position;
            stream.Position = 0;
        }

        try
        {
            byte[] readBuffer = new byte[4096];

            int totalBytesRead = 0;
            int bytesRead;

            while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
            {
                totalBytesRead += bytesRead;

                if (totalBytesRead == readBuffer.Length)
                {
                    int nextByte = stream.ReadByte();
                    if (nextByte != -1)
                    {
                        byte[] expandedBuffer = new byte[readBuffer.Length * 2];
                        Buffer.BlockCopy(readBuffer, 0, expandedBuffer, 0, readBuffer.Length);
                        Buffer.SetByte(expandedBuffer, totalBytesRead, (byte)nextByte);
                        readBuffer = expandedBuffer;
                        totalBytesRead++;
                    }
                }
            }

            byte[] buffer = readBuffer;
            if (readBuffer.Length != totalBytesRead)
            {
                buffer = new byte[totalBytesRead];
                Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
            }
            return buffer;
        }
        finally
        {
            if (stream.CanSeek)
            {
                stream.Position = originalPosition;
            }
        }
    }

}