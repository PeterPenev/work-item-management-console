using Wim.Models.Interfaces;

namespace Wim.Core.Contracts
{
    public interface IInputValidator
    {
        void IsNullOrEmpty(string inputToCheck, string inputType);

        void ValdateMemberNameLength(string inputNameToCheck);

        void ValdateBoardNameLength(string boardNameToCheck);

        void ValdateItemTitleLength(string itemTitleToCheck);

        void ValdateItemDescriptionLength(string itemTitleToCheck);

        void IsEnumConvertable<T>(bool isEnumConvertableBool, T enumTypeForConverting);

        int ValidateRatingConversion(string ratingForCheck);
    }
}
