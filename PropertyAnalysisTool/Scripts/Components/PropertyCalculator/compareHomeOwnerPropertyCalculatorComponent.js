define(['knockout', 'viewModel/HomeOwnerPropertyDetailsViewModel', 'text!components/propertyCalculator/CompareHomeOwnerPropertyCalculatorTemplate.html'],
    function (ko, viewModel, template) {
        ko.components.register('property-calculator', {
            viewModel: viewModel,
            template: template,
        });

        //ko.applyBindings();
    })