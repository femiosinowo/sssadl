using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Ektron.Cms;
using Ektron.Cms.Organization;
using Ektron.Cms.Framework.Organization;
using Ektron.Cms.Instrumentation;
using System.Xml;
using Ektron.Cms.Framework;

namespace SSADL.CMS
{
    /// <summary>
    /// Summary description for MenuHelper
    /// </summary>
    public class MenuHelper
    {
        private static MenuManager menuMngr = new MenuManager(Ektron.Cms.Framework.ApiAccessMode.Admin);

        public MenuHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// Method to get menu data by menu id
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static MenuData GetMenuData(long menuId, bool refreshCache = false)
        {            
            try
            {
                String cacheKey = String.Format("SSADL:MenuId={0}:GetMenuData:CacheBase", menuId);
                if (HttpRuntime.Cache[cacheKey] != null && refreshCache == false) return (MenuData)HttpRuntime.Cache[cacheKey];

                MenuData mData = menuMngr.GetMenu(menuId);
                if (mData != null && mData.Id > 0)
                {
                 //   ApplicationCache.Insert<MenuData>(cacheKey, mData, CacheDuration.For12Hr);
                }
                return mData;
            }
            catch (Exception ex)
            {
                Log.WriteError("MenuHelper > GetMenuData exception!: " + ex + " :: " + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// This method is used to get menu tree with child items
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public static MenuData GetMenuTree(long menuId, bool refreshCache = false)
        {            
            try
            {
                String cacheKey = String.Format("SSADL:MenuId={0}:GetMenuTree:CacheBase", menuId);
                if (HttpRuntime.Cache[cacheKey] != null && refreshCache == false) return (MenuData)HttpRuntime.Cache[cacheKey];

                MenuData mData = menuMngr.GetTree(menuId);
                if (mData != null && mData.Id > 0)
                {
                  //  ApplicationCache.Insert<MenuData>(cacheKey, mData, CacheDuration.For12Hr);
                }
                return mData;
            }
            catch (Exception ex)
            {
                Log.WriteError("MenuHelper > GetMenuTree exception!: " + ex + " :: " + ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// This method is used to get menu data list by
        /// </summary>
        /// <param name="mCriteria"></param>
        /// <returns></returns>
        public static List<MenuData> GetMenuTree(MenuCriteria mCriteria, bool refreshCache = false)
        {            
            try
            {
                String cacheKey = String.Format("SSADL:MenuCriteria={0}:GetMenuTree:CacheBase", mCriteria.ToCacheKey());
                if (HttpRuntime.Cache[cacheKey] != null && refreshCache == false) return (List<MenuData>)HttpRuntime.Cache[cacheKey];

                List<MenuData> dataList = menuMngr.GetMenuList(mCriteria);
                if (dataList != null && dataList.Any())
                {
                   // ApplicationCache.Insert<List<MenuData>>(cacheKey, dataList, CacheDuration.For12Hr);
                }
                return dataList;
            }
            catch (Exception ex)
            {
                Log.WriteError("MenuHelper > GetMenuTree exception!: " + ex + " :: " + ex.StackTrace);
                return null;
            }
        }


        public static string getMenuXML(long menuId, System.Web.UI.Page Page)
        {

          //  XmlDocument xmlDoc = new XmlDocument();
            Ektron.Cms.Controls.FlexMenu flexMenu = new Ektron.Cms.Controls.FlexMenu();
            flexMenu.Page = Page;
            flexMenu.DefaultMenuID = menuId;
            flexMenu.CacheInterval = 3000;
            flexMenu.StartLevel = 0;
            flexMenu.SuppressAddEdit = true;
            flexMenu.Fill();
           
             
            return flexMenu.XmlDoc.InnerXml;
        }
    }
}