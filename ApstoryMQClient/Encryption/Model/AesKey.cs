using System;
using System.Collections.Generic;
using System.Text;

namespace ApStory.PubSub.Client.DotNetStandard.Encryption.Model
{
    public class AESKey
    {
        public string Key { get; set; }        
        public string IV { get; set; }
    }
}
