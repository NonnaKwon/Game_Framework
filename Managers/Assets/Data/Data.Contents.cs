using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    
    #region BaseData

    public class BaseData
    {
        public int TemplateId;
        public string Name; //개발용
        public string NameTextId;
        public string DescriptionTextID;
        public string IconImage;
        public string PrefabName;
    }


    #endregion

    #region TextData

    [Serializable]
    public class TextData
    {
        public string TemplateId;
        public string KOR;
    }

    [Serializable]
    public class TextDataLoader : ILoader<string, TextData>
    {
        public List<TextData> texts = new List<TextData>();

        public Dictionary<string, TextData> MakeDict()
        {
            Dictionary<string, TextData> dict = new Dictionary<string, TextData>();
            foreach (TextData text in texts)
                dict.Add(text.TemplateId, text);

            return dict;
        }

        public bool Validate()
        {
            return true;
        }
    }

    #endregion

}