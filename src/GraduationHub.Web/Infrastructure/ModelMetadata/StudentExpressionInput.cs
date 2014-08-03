using System;
using System.Web.Mvc;

namespace GraduationHub.Web.Infrastructure.ModelMetadata
{
    public class StudentExpressionInput : Attribute, IMetadataAware
    {
        private readonly int _fieldLength;

        public StudentExpressionInput(int fieldLength)
        {
            _fieldLength = fieldLength;
        }

        public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
            metadata.AdditionalValues.Add("_STUDENTEXPRESSIONLENGTH", _fieldLength);
        }
    }
}