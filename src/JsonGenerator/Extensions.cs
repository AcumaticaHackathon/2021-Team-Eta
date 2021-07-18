using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Configuration;
using PX.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hackathon.Eta.JsonGenerator
{
    /// <summary>
    /// Misc. extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Remove a node from the JSON data
        /// </summary>
        /// <param name="dataObj">top JSON node</param>
        /// <param name="nodeName">node name to remove</param>
        public static void RemoveNode(this JObject dataObj, string nodeName)
        {
            JProperty nodeProp = dataObj.Property(nodeName);

            #region logging
            //log.Debug(string.Format("Extensions.RemoveNode: nodeName = [{0}]", nodeName));
            #endregion

            if (nodeProp != null)
            {
                nodeProp.Remove();
            }
        }

        /// <summary>
        /// Remove nodes from the JSON data in the included list
        /// </summary>
        /// <param name="dataObj">top JSON node</param>
        /// <param name="list">name list to remove</param>
        public static void RemoveNodes(this JObject dataObj, string[] list)
        {
            List<JProperty> toRemove = new List<JProperty>();

            // Build list of nodes to remove
            foreach (JProperty nodeProp in dataObj.Properties())
            {
                if (list.Contains(nodeProp.Name))
                {
                    toRemove.Add(nodeProp);
                }
            }

            // Remove the nodes
            foreach (JProperty nodeProp in toRemove)
            {
                nodeProp.Remove();
            }
        }

        /// <summary>
        /// Remove nodes from the JSON data other than the included list
        /// </summary>
        /// <param name="dataObj">top JSON node</param>
        /// <param name="list">name list to keep</param>
        public static void RemoveOtherNodes(this JObject dataObj, string[] list)
        {
            List<JProperty> toRemove = new List<JProperty>();

            // Build list of nodes to remove
            foreach (JProperty nodeProp in dataObj.Properties())
            {
                if (!list.Contains(nodeProp.Name))
                {
                    toRemove.Add(nodeProp);
                }
            }

            // Remove the nodes
            foreach (JProperty nodeProp in toRemove)
            {
                nodeProp.Remove();
            }
        }

        /// <summary>
        /// Set the value of a JSON node
        /// </summary>
        /// <param name="dataObj">top JSON node</param>
        /// <param name="nodeName">node name to set</param>
        /// <param name="nodeValue">node value to set</param>
        public static void SetNodeValue(this JObject dataObj, string nodeName, string nodeValue)
        {
            JProperty nodeProp = dataObj.Property(nodeName);
            JObject valueObj;

            #region logging
            //log.Debug(string.Format("Extensions.SetNodeValue: nodeName = [{0}]", nodeName));
            #endregion

            if (nodeProp != null)
            {
                valueObj = (JObject)nodeProp.Value;

                if (valueObj != null)
                {
                    #region logging
                    //log.Debug(string.Format("Extensions.SetNodeValue: valueObj.Properties().Count() = [{0}]", valueObj.Properties().Count()));
                    #endregion

                    JProperty valueProp = valueObj.Properties().FirstOrDefault();

                    if (valueProp != null)
                    {
                        valueProp.Value = nodeValue;

                        #region logging
                        //log.Debug(string.Format("Extensions.SetNodeValue: valueProp = [{0}]", valueProp.Value));
                        #endregion

                        // Replace date with new value
                        nodeProp.Value = valueObj;
                    }
                }
            }
        }

        /// <summary>
        /// Add a JSON node
        /// </summary>
        /// <param name="dataObj">top JSON node</param>
        /// <param name="nodeName">node name to set</param>
        /// <param name="nodeValue">node value to set</param>
        public static void AddNode(this JObject dataObj, string nodeName, string nodeValue)
        {
            #region logging
            //log.Debug(string.Format("Extensions.AddNode: nodeName = [{0}]", nodeName));
            //log.Debug(string.Format("Extensions.AddNode: nodeValue = [{0}]", nodeValue));
            #endregion

            dataObj.Add(nodeName, new JObject(
                    new JProperty("value", nodeValue)));
        }

        /// <summary>
        /// Retrieve the value of a JSON node
        /// </summary>
        /// <param name="dataObj">top JSON node</param>
        /// <param name="nodeName">node name to retrieve</param>
        /// <returns></returns>
        public static string GetNodeValue(this JObject dataObj, string nodeName)
        {
            JProperty nodeProp = dataObj.Property(nodeName);
            JObject valueObj;
            string nodeValue = null;

            #region logging
            //log.Debug(string.Format("Extensions.GetNodeValue: nodeName = [{0}]", nodeName));
            #endregion

            if (nodeProp != null)
            {
                valueObj = (JObject)nodeProp.Value;

                if (valueObj != null)
                {
                    #region logging
                    //log.Debug(string.Format("Extensions.GetNodeValue: valueObj.Properties().Count() = [{0}]", valueObj.Properties().Count()));
                    #endregion

                    JProperty valueProp = valueObj.Properties().FirstOrDefault();

                    if (valueProp != null)
                    {
                        #region logging
                        //log.Debug(string.Format("Extensions.GetNodeValue: valueProp = [{0}]", valueProp.Value));
                        #endregion

                        nodeValue = (string)valueProp.Value;
                    }
                }
            }

            return nodeValue;
        }
    }
}
