ko.bindingHandlers.propertyCalculatorText = {
    init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        // This will be called when the binding is first applied to an element
        // Set up any initial state, event handlers, etc. here

        $(element).keyup(function(e)
        {
            var value = valueAccessor();
            var val = this.value.replace(/\D/g, '');
            //$(element).val("$" + );
            value(val);

        })
    },
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        // This will be called once when the binding is first applied to an element,
        // and again whenever any observables/computeds that are accessed change
        // Update the DOM element based on the supplied values here.

        var value = ko.unwrap(valueAccessor());

        value = parseInt(value);

        value = isNaN(value) ? 0 : value;
        
        if (element.localName === "input") {
            $(element).val('$' + value.toString());
        }
        else {
            $(element).text('$' + value.toString()).css('color', 'black');
        }

        if (value < 0)
        {
            $(element).css('color', 'red');
        }

    }
};
