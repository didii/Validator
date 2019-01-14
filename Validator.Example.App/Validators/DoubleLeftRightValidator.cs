﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Example.App.Models;

namespace Validator.Example.App.Validators {
    class DoubleLeftRightValidator : SectionedValidator<DoubleModel, DoubleModel> {
        public const string InInSection = "type.inin";
        public const string InOutSection = "type.inout";

        public DoubleLeftRightValidator() {
            Section(InInSection, ValidateInIn);
            Section(InOutSection, ValidateInOut);
        }

        private void ValidateInIn(DoubleModel left, DoubleModel right) {
            //Rule: If Type for left and right is In, name must be the same for both
            Start(new ValidationInfo("e229be44-ec96-436a-916c-dbbfabe7a7f3", "When In is selected for both Left and Right, Left and Right should have the same name", "doubleleftright"));
            if (left.Name != right.Name)
                Fail();
            Complete(Assume.Pass);
        }

        private void ValidateInOut(DoubleModel left, DoubleModel right) {
            //Rule: If Type for left is In and Out for right, Left must be null
            Start(new ValidationInfo("66a73e51-1c21-4ef7-958c-ae45acfd9f45", "When In-Out, Left must be null"));
            if (left.Name != null)
                Fail();
            Complete(Assume.Pass);

            //Rule: If Type for left is In and Out for right, Right must have a non-null and non-empty value
            Start(new ValidationInfo("670ac060-d954-4518-8d03-155130391b92", "When In-Out, Right must have a name"));
            if (string.IsNullOrEmpty(right.Name))
                Fail();
            Complete(Assume.Pass);
        }
    }
}