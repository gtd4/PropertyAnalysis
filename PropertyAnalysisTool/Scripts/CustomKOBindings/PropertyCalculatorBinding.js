ko.bindingHandlers.propertyCalculatorText = {
    //init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
    //    // This will be called when the binding is first applied to an element
    //    // Set up any initial state, event handlers, etc. here
    //},
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        // This will be called once when the binding is first applied to an element,
        // and again whenever any observables/computeds that are accessed change
        // Update the DOM element based on the supplied values here.
        var value = ko.unwrap(valueAccessor());

        value = parseInt(value);

        vaulue = isNaN(value) ? 0 : value;

        $(element).text('$' + value.toString()).css('color','black');
        debugger;
        if(value < 0)
        {
            $(element).css('color', 'red');
        }
    }
};