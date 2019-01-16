﻿using System;
using System.ComponentModel.DataAnnotations;
using FlexValidator.Example.App.Models;
using FlexValidator.Example.App.Validators;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FlexValidator.Example.App {
    class Program {
        static void Main(string[] args) {
            var model = CreateModel();
            Console.WriteLine();

            var validationResult = ValidateModel(model);

            LogResults(validationResult);
        }

        private static SomeModel CreateModel() {
            var model = new SomeModel() {
                Id = -5,
                Name = "didii",
                Sub = new SubModel() {
                    Id = 0,
                    Name = "",
                    Type = SubModelType.Allowed
                },
                DoubleLeft = new DoubleModel() {
                    Name = "A name",
                    Type = DoubleModelType.In
                },
                DoubleRight = new DoubleModel() {
                    Name = "Another name",
                    Type = DoubleModelType.In
                }
            };
            Console.WriteLine("Using the following model:");
            Console.WriteLine(JsonConvert.SerializeObject(model, new JsonSerializerSettings() {
                Formatting = Formatting.Indented,
                Converters = { new StringEnumConverter() }
            }));
            return model;
        }

        private static ValidationResult ValidateModel(SomeModel model) {
            var validator = new SomeModelValidator();
            return validator.Validate(model);
        }

        private static void LogResults(ValidationResult result) {
            if (result.IsValid) {
                Console.WriteLine("Validation succeeded");
                return;
            }
            foreach (var fail in result.Fails)
                Console.WriteLine($"{fail.Guid}: {fail.Message}");
        }
    }
}