define(['knockout','viewModel/PropertyDetailsViewModel', 'text!components/propertyCalculator/ComparePropertyCalculatorTemplate.html'],
    function (ko, viewModel, template) {


        ko.components.register('property-calculator', {
            viewModel: viewModel,
            template: template,
        });

        //ko.applyBindings();
    })

