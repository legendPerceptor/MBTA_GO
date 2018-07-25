using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;
using System.Linq;
using System.IO;

namespace MyMBTAApp.Views
{
    public partial class PracticeLayouts : ContentPage
    {
        AbsoluteLayout absoluteLayout;
        public PracticeLayouts()
        {
            //InitializeComponent();
            ChessBoxDynamicPage();
        }

        void On5JobButtonClicked(object sender, EventArgs args){
        }

        void ChessBoxDynamicPage(){
            absoluteLayout = new AbsoluteLayout
            {
                BackgroundColor = Color.FromRgb(240, 220, 130),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            for (int i = 0; i < 32;i++){
                
                    BoxView boxView = new BoxView
                    {
                        Color = Color.FromRgb(0, 64, 0)
                    };
                    absoluteLayout.Children.Add(boxView);
                
            }
            ContentView contentView = new ContentView
            {
                Content = absoluteLayout
            };

            contentView.SizeChanged += ChangeChessBoxWhenScreenSizeChange;
            this.Padding = new Thickness(5, Device.OnPlatform(20, 0, 0), 5, 5);
            this.Content = contentView;

        }
        void ChangeChessBoxWhenScreenSizeChange(object sender,EventArgs args){
            ContentView view = (ContentView)sender;
            double squareSize = Math.Min(view.Width, view.Height) / 8;
            int index = 0;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    if (((row ^ col) & 1) == 0)
                    {
                        continue;
                    }

                    Rectangle rectangle = new Rectangle(col * squareSize, row * squareSize, squareSize, squareSize);
                    View childView = absoluteLayout.Children[index];
                    AbsoluteLayout.SetLayoutBounds(childView, rectangle);
                    index++;
                }
            }

        }
        void ChessBoxFixedPage(){
            const double squareSize = 35;
            AbsoluteLayout absoluteLayout = new AbsoluteLayout
            {
                BackgroundColor = Color.FromRgb(240, 220, 130),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            for (int row = 0; row < 8;row++){
                for (int col = 0; col < 8;col++){
                    if(((row ^ col) & 1 )==0){
                        continue;
                    }
                    BoxView boxView = new BoxView
                    {
                        Color = Color.FromRgb(0, 64, 0)
                    };
                    Rectangle rectangle = new Rectangle(col * squareSize, row * squareSize, squareSize, squareSize);
                    absoluteLayout.Children.Add(boxView,rectangle);
                }
            }
            this.Content = absoluteLayout;
        }




        void VerticalOptionDemo()
        {
            Color[] colors = { Color.Yellow, Color.Blue };
            int flipFlopper = 0;

            //Create Labels sorted by LayoutAlignment property
            IEnumerable<Label> labels = from field in typeof(LayoutOptions).GetRuntimeFields()
                                        where field.IsPublic && field.IsStatic
                                        orderby ((LayoutOptions)field.GetValue(null)).Alignment
                                        select new Label
                                        {
                                            Text = "VerticalOptions = " + field.Name,
                                            VerticalOptions = (LayoutOptions)field.GetValue(null),
                                            HorizontalTextAlignment = TextAlignment.Center,
                                            FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                                            TextColor = colors[flipFlopper],
                                            BackgroundColor = colors[flipFlopper = 1 - flipFlopper]
                                        };
            StackLayout stackLayout = new StackLayout();
            foreach (Label label in labels)
            {
                stackLayout.Children.Add(label);
            }
            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            Content = stackLayout;
        }


        void ScrollViewOfColors()
        {

            StackLayout stackLayout = new StackLayout();
            // Loop through the Color structure fields.
            foreach (FieldInfo info in typeof(Color).GetRuntimeFields())
            {
                // Skip the obsolete (i.e. misspelled) colors.
                if (info.GetCustomAttribute<ObsoleteAttribute>() != null)
                    continue;
                if (info.IsPublic &&
                    info.IsStatic &&
                    info.FieldType == typeof(Color))
                {
                    stackLayout.Children.Add(
                          ClearColorView((Color)info.GetValue(null), info.Name));
                }
            }
            // Loop through the Color structure properties.
            foreach (PropertyInfo info in typeof(Color).GetRuntimeProperties())
            {
                MethodInfo methodInfo = info.GetMethod;
                if (methodInfo.IsPublic &&
                   methodInfo.IsStatic &&
                    methodInfo.ReturnType == typeof(Color))
                {

                    stackLayout.Children.Add(
                                ClearColorView((Color)info.GetValue(null), info.Name));
                }
            }
            Padding = new Thickness(5, Device.OnPlatform(20, 5, 5), 5, 5);
            // Put the StackLayout in a ScrollView.
            Content = new ScrollView
            {
                Content = stackLayout
            };
        }

        View ClearColorView(Color color, string name)
        {
            return new Frame
            {
                OutlineColor = Color.Accent,
                Padding = new Thickness(5),
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 15,
                    Children ={
                        new BoxView{
                            Color = color
                        },
                        new Label{
                            Text = name,
                            FontSize = Device.GetNamedSize(NamedSize.Large,typeof(Label)),
                            FontAttributes = FontAttributes.Bold,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        },
                        new StackLayout{
                            Children={
                                new Label{
                                    Text = String.Format("{0:X2}-{1:X2}-{2:X2}",
                                    (int)(255 * color.R),
                                    (int)(255 * color.G),
                                    (int)(255 * color.B)),
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    IsVisible = color != Color.Default
                                },
                                new Label{
                                    Text = String.Format("{0:F2}, {1:F2}, {2:F2}",
                                    color.Hue,
                                    color.Saturation,
                                    color.Luminosity),
                                    VerticalOptions = LayoutOptions.CenterAndExpand,
                                    IsVisible = color != Color.Default
                                }
                            },
                            HorizontalOptions=LayoutOptions.End
                        }

                    }
                }
            };
        }

        //ScrollView inside a StackLayout
        void HarvardPage()
        {
            StackLayout mainStack = new StackLayout();
            StackLayout textStack = new StackLayout
            {
                Padding = new Thickness(5),
                Spacing = 10
            };

            //Get access to the text resource
            Assembly assembly = GetType().GetTypeInfo().Assembly;
            string resourceID = "MyMBTAApp.Texts.article.txt";
            using (Stream stream = assembly.GetManifestResourceStream(resourceID))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    bool gotTitle = false;
                    string line;

                    while (null != (line = reader.ReadLine()))
                    {
                        Label label = new Label
                        {
                            Text = line,
                            TextColor = Color.Black
                        };
                        if (!gotTitle)
                        {
                            label.HorizontalOptions = LayoutOptions.Center;
                            label.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));
                            label.FontAttributes = FontAttributes.Bold;
                            mainStack.Children.Add(label);
                            gotTitle = true;
                        }
                        else
                        {
                            textStack.Children.Add(label);
                        }
                    }
                }
            }
            ScrollView scrollView = new ScrollView
            {
                Content = textStack,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(5)

            };
            mainStack.Children.Add(scrollView);
            Content = mainStack;
            BackgroundColor = Color.White;
            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);

        }


    }
}
