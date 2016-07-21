define(['knockout', 'jquery'],
    function (ko) {// Look into making it more generic,
        ko.bindingHandlers.propertyCalculatorText = {
            init: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
                var wrapper = ko.computed({
                    read: function () {
                        return "$" + ko.unwrap(valueAccessor());
                    },
                    write: function (value) {
                        var numeric = parseInt(value.replace(/\$/g, ""));

                        valueAccessor()(isNaN(numeric) ? 0 : numeric);
                        //valueAccessor().valueHasMutated();
                    }
                }).extend({ notify: 'always' });

                var colourComputed = ko.computed(function () {
                    return ko.unwrap(valueAccessor()) < 0 ? 'red' : 'black';
                });

                if (element.localName === "input") {
                    ko.applyBindingsToNode(element, { value: wrapper, valueUpdate: 'keyup' });
                } else {
                    ko.applyBindingsToNode(element, { text: wrapper, style: { color: colourComputed } });
                }

                
            },
            update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
               
            }
        };

    });
