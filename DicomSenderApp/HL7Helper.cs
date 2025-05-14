using System;
using System.IO;
using System.Text;
using NHapi.Base.Parser;
using NHapi.Base.Model;
using NHapi.Model.V23.Message;

public class HL7Helper
{
    public static IMessage LoadAndParseHL7(string filePath)
    {
        try
        {
            // Read file content as bytes first to avoid encoding issues
            byte[] fileBytes = File.ReadAllBytes(filePath);
            
            // Debug output to see what's in the file
            if (fileBytes.Length > 0)
            {
                Console.WriteLine($"First 10 bytes: {BitConverter.ToString(fileBytes, 0, Math.Min(10, fileBytes.Length))}");
            }
            
            // Determine if the file has a BOM
            bool hasBom = HasByteOrderMark(fileBytes);
            
            // Convert to string carefully, skip BOM if present
            string rawContent;
            if (hasBom)
            {
                // Skip the BOM (first 3 bytes for UTF-8)
                rawContent = Encoding.UTF8.GetString(fileBytes, 3, fileBytes.Length - 3);
                Console.WriteLine("Detected and removed BOM marker");
            }
            else
            {
                // No BOM, use the entire content
                rawContent = Encoding.UTF8.GetString(fileBytes);
            }
            
            // Debug output
            if (rawContent.Length > 0)
            {
                Console.WriteLine($"First 10 chars: {rawContent.Substring(0, Math.Min(10, rawContent.Length))}");
                
                // Check if the first char is 'M' as expected for HL7 MSH segment
                if (!rawContent.StartsWith("MSH|"))
                {
                    Console.WriteLine($"WARNING: HL7 content does not start with 'MSH|', first chars: '{rawContent.Substring(0, Math.Min(20, rawContent.Length))}'");
                    
                    // If it's missing just the 'M', let's try to fix it
                    if (rawContent.StartsWith("SH|"))
                    {
                        Console.WriteLine("Found 'SH|' instead of 'MSH|' - prepending missing 'M'");
                        rawContent = "M" + rawContent;
                    }
                }
            }
            
            // Clean up common issues with HL7 files
            // This seems to mess things up
            //rawContent = CleanupHL7Content(rawContent);
            
            // Create a pipe parser
            PipeParser parser = new PipeParser();
            
            // Parse the message
            IMessage message = parser.Parse(rawContent);
            
            // Log basic message info
            Console.WriteLine($"Successfully parsed HL7 message of type: {message.GetStructureName()}");
            
            return message;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to parse HL7 message: {ex.Message}", ex);
        }
    }
    
    private static bool HasByteOrderMark(byte[] fileBytes)
    {
        // Check for UTF-8 BOM (0xEF, 0xBB, 0xBF)
        if (fileBytes.Length >= 3 && 
            fileBytes[0] == 0xEF && 
            fileBytes[1] == 0xBB && 
            fileBytes[2] == 0xBF)
        {
            return true;
        }
        
        // Check for UTF-16 LE BOM (0xFF, 0xFE)
        if (fileBytes.Length >= 2 &&
            fileBytes[0] == 0xFF &&
            fileBytes[1] == 0xFE)
        {
            // This is more complex - we'd need to handle UTF-16 differently
            Console.WriteLine("WARNING: File appears to be UTF-16 LE encoded");
        }
        
        // Check for UTF-16 BE BOM (0xFE, 0xFF)
        if (fileBytes.Length >= 2 &&
            fileBytes[0] == 0xFE &&
            fileBytes[1] == 0xFF)
        {
            Console.WriteLine("WARNING: File appears to be UTF-16 BE encoded");
        }
        
        return false;
    }
    
    public static string CleanupHL7Content(string content)
    {
        if (string.IsNullOrEmpty(content))
            return content;
        
        // Remove any UTF-8 BOM
        if (content.StartsWith("\uFEFF"))
            content = content.Substring(1);
            
        // Normalize line endings to CRLF as expected by most HL7 parsers
        content = content.Replace("\r\n", "\r").Replace("\n", "\r").Replace("\r", "\r\n");
        
        // Trim any leading/trailing whitespace
        content = content.Trim();
        
        return content;
    }
    
    // Helper method for ORM_O01 message accessing 
    public static ORM_O01 GetORM_O01Message(IMessage message)
    {
        if (message is ORM_O01 ormMessage)
        {
            return ormMessage;
        }
        else
        {
            throw new Exception("Message is not an ORM_O01 message");
        }
    }
    
    // Helper method to create formatted presentation of a message
    public static string GetMessageDisplay(IMessage message)
    {
        if (message == null) return "No message to display";
        
        try
        {
            PipeParser parser = new PipeParser();
            return parser.Encode(message);
        }
        catch (Exception ex)
        {
            return $"Error formatting message: {ex.Message}";
        }
    }
}
