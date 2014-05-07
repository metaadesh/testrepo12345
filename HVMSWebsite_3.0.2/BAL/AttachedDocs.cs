using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace METAOPTION.BAL
{
   public class AttachedDocs
   {
      private Int32 _documentId;
      private String _fileName;
      private String _fileType;
      private Byte[] _fileBytes;
      private Int32 _fileLength;

      public Int32 DocumentId
      {
         get { return this._documentId; }
         set { this._documentId = value; }
      }
      public String FileName
      {
         get { return this._fileName; }
         set { this._fileName = value; }
      }
      public String FileType
      {
         get { return this._fileType; }
         set { this._fileType = value; }
      }
      public Byte[] FileBytes
      {
         get { return this._fileBytes; }
         set { this._fileBytes = value; }
      }
      public Int32 FileLength
      {
         get { return this._fileLength; }
         set { this._fileLength = value; }
      }
   }
}