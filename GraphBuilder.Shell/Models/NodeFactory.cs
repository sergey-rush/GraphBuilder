using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GraphBuilder.Core;
using GraphBuilder.Shell.ViewModels;

namespace GraphBuilder.Shell.Models
{
    public static class NodeFactory
    {
        private static Random random;
        private static List<AppColor> colors;
        static NodeFactory()
        {
            colors = new AppColorList();
            random = new Random();
        }

        public static Node CreateNode(Point point, ShapeType shapeType)
        {
            Color borderColor= Colors.Black;
            Color shapeColor = Colors.White;
            return CreateNode(point, shapeType, borderColor, shapeColor);
        }

        public static Node CreateNode(Point point, ShapeType shapeType, Color borderColor, Color shapeColor)
        {
            
            Node node = new Node();
            node.Width = 40;
            node.Height = 40;
            node.Location = new Point(point.X - (node.Width / 2), point.Y - (node.Height / 2));
            node.ShapeType = shapeType;
            node.Label = "N";
            node.Name = "Full Name";
            node.Header = "Объект";
            node.Info = "Мастер получает задание выяснить, кто из высших руководителей ведёт сепаратные переговоры.";
            node.BorderColor = borderColor;
            node.StrokeThickness = 2;
            node.ShapeColor = shapeColor;
            node.Cards = CreateCards(1);
            node.Attributes = CreateAttributes();
            node.Init();
            return node;
        }

        public static List<Node> CreateNodes(int nodesNumber)
        {
            
            List<Node> nodes = new List<Node>();
            List<Point> points = GetPointsOnCircle(nodesNumber, 300, new Point(400, 400));

            for (int i = 1; i <= nodesNumber; i++)
            {
                AppColor appColor = colors.PickRandom();
                Point point = points[i - 1];
                Node node = CreateNode(point, ShapeType.Ellipse, appColor.BorderColor, appColor.ShapeColor);
                node.Label = i.ToString();
                node.Index = i;
                nodes.Add(node);
            }

            return nodes;
        }

        private static List<ListItem> CreateAttributes()
        {
            List<ListItem> attributes = new List<ListItem>();
            attributes.Add(new ListItem("Орган", "Министерство"));
            attributes.Add(new ListItem("Должность", "Сотрудник"));
            attributes.Add(new ListItem("Учетная карта", "Number"));
            attributes.Add(new ListItem("Источник", "Штатный"));
            attributes.Add(new ListItem("Метод", "Идеологический"));
            attributes.Add(new ListItem("Категория", "Работник"));
            attributes.Add(new ListItem("Мастер", "Психологический парадокс"));
            attributes.Add(new ListItem("Доверие", "Групповое мышление"));
            attributes.Add(new ListItem("Опасность ", "Утечки"));
            return attributes;
        }

        private static List<Card> CreateCards(int index)
        {
            List<Card> cards = new List<Card>();
            for (int i = 0; i < index; i++)
            {
                Card card = new Card();
                
                card.Date = DateTime.Now;
                
                card.Header = String.Format("Карточка № {0}/{1}", i, index);
                card.Index = i;
                card.Info = "Агент это лицо, не являющееся штатным сотрудником разведывательной службы, но действующее по заданиям этой службы.";
                card.Score = i*index;
                card.Source = "Публичные данные";
                card.Summary =
                    "Разведка, проводимая агентами в мирное и в военное время с цепью получения секретных данных о вооружённых силах, военном потенциале и других сведений, составляющих государственную и военную тайну страны, против которой ведётся разведка.";
                cards.Add(card);
            }
            return cards;
        }


        /// <summary>
        /// GetPointsOnCircle
        /// </summary>
        /// <param name="length">Number of points</param>
        /// <param name="radius">Radius of the circle</param>
        /// <param name="center">Center point</param>
        /// <returns></returns>
        private static List<Point> GetPointsOnCircle(int length, double radius, Point center)
        {
            List<Point> points = new List<Point>();
            double slice = 2 * Math.PI / length;
            for (int i = 0; i < length; i++)
            {
                double angle = slice * i;
                int newX = (int)(center.X + radius * Math.Cos(angle));
                int newY = (int)(center.Y + radius * Math.Sin(angle));
                Point p = new Point(newX, newY);
                points.Add(p);
            }

            return points;
        }
    }
}