using iS3.ImportTools.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iS3.ImportTools.DataStanardTool.StandardManager
{
    public class StandardFilter
    {
        public List<Tunnel> Tunnels { get; set; }
        public StandardFilter()
        {
            Tunnels = new List<Tunnel>();
        }


        /// <summary>
        /// generate New DateStandard by filter conditon
        /// </summary>
        /// <param name="dataStandard">Common DataStand with all items in it </param>
        /// <param name="tunnelType">tunnel type in Chinese </param>
        /// <param name="constructionStage"></param>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public PmEntiretyDef Filter(PmEntiretyDef dataStandard, string tunnelType = null, string constructionStage = null, string categoryName = null)
        {
            try
            {
                if (tunnelType != null)
                {
                    Tunnel tunnel = Tunnels.Find(x => x.LangStr == tunnelType);
                    PmEntiretyDef newStandard = new PmEntiretyDef()
                    {
                        Code = tunnel.TunnelType,
                        LangStr = tunnel.LangStr
                    };

                    if (constructionStage != null)
                    {
                        Stage stage = tunnel.Stages.Find(x => x.LangStr == constructionStage);
                        if (categoryName != null)
                        {
                            Category category = stage.Categories.Find(x => x.LangStr == categoryName);
                            Filter2Standard(category, ref newStandard, dataStandard);
                        }
                        else
                        {
                            foreach (var item in stage.Categories)
                            {
                                Filter2Standard(item, ref newStandard, dataStandard);
                            }
                        }
                        return newStandard;
                    }
                    else
                    {
                        foreach (Stage stage in tunnel.Stages)
                        {
                            foreach (Category category in stage.Categories)
                                Filter2Standard(category, ref newStandard, dataStandard);
                        }
                        return newStandard;
                    }
                }
                else
                {
                    PmEntiretyDef newStandard = new PmEntiretyDef() { Code = dataStandard.Code, LangStr = dataStandard.LangStr };
                    foreach (Tunnel tunnel in Tunnels)
                    {
                        foreach (Stage stage in tunnel.Stages)
                        {
                            foreach (Category category in stage.Categories)
                            {
                                Filter2Standard(category, ref newStandard, dataStandard);
                            }

                        }
                    }
                    return newStandard;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void Filter2Standard(Category category, ref PmEntiretyDef standardDef, PmEntiretyDef dataStandard)
        {
            PmDomainDef domain = new PmDomainDef()
            {
                Code = category.CategoryName,
                LangStr = category.LangStr
            };
            foreach (string obj in category.objList)
            {
                PmDGObjectDef objectDef = dataStandard.GetDGObjectDefByCode(obj);
                domain.DGObjectContainer.Add(objectDef);
            }
            standardDef.DomainContainer.Add(domain);

        }

        public Category GetCategoryByName(string categoryName)
        {
            foreach (Tunnel tunnel in Tunnels)
            {
                foreach (Stage stage in tunnel.Stages)
                {
                    return stage.Categories.Find(x => x.LangStr == categoryName);
                }
            }
            return null;
        }
    }

    public class Tunnel : LangBase
    {
        public string TunnelType { get; set; }
        public List<Stage> Stages { get; set; }
        public Tunnel(string tunnelType, string Langstr = null)
        {
            this.TunnelType = tunnelType;
            this.LangStr = Langstr;
            this.Stages = new List<Stage>();
        }
        public Tunnel()
        {
            this.Stages = new List<Stage>();
        }
    }
    public class Stage : LangBase
    {
        public string StageName { get; set; }
        public List<Category> Categories { get; set; }
        public Stage(string name, string langstr = null)
        {
            this.StageName = name;
            LangStr = langstr;
            Categories = new List<Category>();
        }
        public Stage()
        {
            Categories = new List<Category>();
        }
    }
    public class Category : LangBase
    {
        public string CategoryName { get; set; }
        public List<string> objList { get; set; }
        public Category()
        {
            objList = new List<string>();
        }
        public Category(string catagoryName, string langstr = null)
        {
            objList = new List<string>();
            LangStr = langstr;
            CategoryName = catagoryName;
        }
    }
}
