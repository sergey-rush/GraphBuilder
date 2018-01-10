using System.Collections.Generic;
using System.Linq;
using GraphBuilder.Core;

namespace GraphBuilder.Shell.Models
{
    public class LinkFactory
    {
        public static List<Node> Nodes { get; set; }

        public static List<Link> CreateLinks(int linksNumber)
        {
            List<Link> links = new List<Link>();
            for (int i = 1; i <= linksNumber; i++)
            {
                List<Node> nodes = Nodes.PickRandom(2).ToList();
                Node startNode = nodes[0];
                Node endNode = nodes[1];

                Link link = new Link();
                link.NodeFrom = startNode.UId;
                link.StartPoint = startNode.ActualPoint;
                link.LineColor = startNode.ShapeColor;
                link.NodeTo = endNode.UId;
                link.EndPoint = endNode.ActualPoint;
                startNode.Links.Add(link);
                endNode.Links.Add(link);
                links.Add(link);
            }
            return links;
        }
    }
}