using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MblexPrep
{
    public class FlipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isFlipped && parameter is View card)
        {
            return isFlipped ? ((FlashcardViewModel)card.BindingContext)?.AnswerText : ((FlashcardViewModel)card.BindingContext)?.QuestionText;
        }

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
    }
}
