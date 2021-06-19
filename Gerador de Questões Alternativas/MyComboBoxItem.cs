using System;
using System.Collections.Generic;
using System.Text;

namespace Gerador_de_Questões_Alternativas
{
  public class MyComboBoxItem
        {
            string _Value = "";
            string _Text = "";

            public MyComboBoxItem(string Value, string Text)
            {
                _Value = Value;
                _Text = Text;
            }

            public string Value
            {
                get
                {
                    return _Value;
                }
                set
                {
                    _Value = value;
                }
            }

            public string Text
            {
                get
                {
                    return _Text;
                }
                set
                {
                    _Text = value;
                }
            }
        }
}
