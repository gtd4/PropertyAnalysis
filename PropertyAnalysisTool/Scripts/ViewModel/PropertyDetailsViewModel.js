var PropertyDetailsViewModel = function (data) {
    var _vm = {};

    ko.mapping.fromJS(data, null, _vm);

    _vm.calculatedPrice = ko.pureComputed(
        {
            read: function () {
                return _vm.price();
            },
            write: function (value) {
                _vm.price(_vm.processWrittenValueInt(value));

                _vm.UpdateRentAndPropertyManagementCosts();
            },

        });

    _vm.calculatedYield = ko.pureComputed({
        read: function () {

            return _vm.initialYieldPercentage();
        },
        write: function (value) {
            var type = true;
            _vm.initialYieldPercentage(_vm.processWrittenValueFloat(value), type);

            _vm.UpdateRentAndPropertyManagementCosts();
        },

    });

    _vm.calculatedRent = ko.pureComputed(
    {
        read: function () {

            return _vm.initialRent();
        },
        write: function (value) {
            _vm.initialRent(_vm.processWrittenValueInt(value));
            var calcYield = Math.round(_vm.initialRent() * (52 - _vm.initialVacancyRate()) / _vm.price() * 100);
            _vm.initialYieldPercentage(calcYield);

            var calcPMCost = _vm.UpdatePropertyManagement();
            _vm.propertyManagementAmount(calcPMCost);

            _vm.proposedAnnualRentalIncome(_vm.CalculateAnnualRent());
        }
    });

    _vm.calculatedVacancyRate = ko.pureComputed(
           {
               read: function () {
                   return _vm.initialVacancyRate();
               },
               write: function (value) {
                   _vm.initialVacancyRate(_vm.processWrittenValueInt(value));

                   _vm.UpdateRentAndPropertyManagementCosts();
               },
           });

    _vm.calculatedInterestRate = ko.pureComputed(
        {
            read: function () {
                return _vm.initialInterestRate();
            },
            write: function (value) {

                _vm.initialInterestRate(_vm.processWrittenValueFloat(value), true);
                var annualInterest = Math.round(_vm.price() * _vm.initialInterestRate() / 100);
                _vm.annualInterestCost(annualInterest);

            }
        });

    _vm.calculatedAnnualInterestAmount = ko.computed(function () {

        var annualInterest = Math.round(_vm.price() * _vm.initialInterestRate() / 100);
        _vm.annualInterestCost(annualInterest);

        return _vm.annualInterestCost();

    });

    _vm.calculatedRentToCoverInterest = ko.computed(function () {
        return Math.round(_vm.annualInterestCost() / (52 - _vm.initialVacancyRate()));
    });

    _vm.calculateSurplusBeforeExpenses = ko.computed(function () {
        return Math.round(_vm.initialRent() - _vm.calculatedRentToCoverInterest());
    });

    _vm.calculatedRates = ko.pureComputed(
        {
            read: function () {
                return _vm.initialRates();
            },
            write: function (value) {

                _vm.initialRates(_vm.processWrittenValueInt(value));

            }
        });

    _vm.calculatedMaintenance = ko.pureComputed(
        {
            read: function () {
                return _vm.initialRepairs();
            },
            write: function (value) {
                _vm.initialRepairs(_vm.processWrittenValueInt(value));

            },
        });

    _vm.calculatedInsurance = ko.pureComputed(
        {
            read: function () {
                return _vm.initialInsurance();
            },
            write: function (value) {
                _vm.initialInsurance(_vm.processWrittenValueInt(value));
            },
        });

    _vm.calculatedPropertyManagement = ko.pureComputed(
        {
            read: function () {

                return _vm.propertyManagementAmount();
            },
            write: function (value) {
                _vm.propertyManagementAmount(_vm.processWrittenValueInt(value));
            },
        });

    _vm.calculatedTotalExpense = ko.computed(function () {

        _vm.totalInitialExpense(_vm.initialRates() + _vm.initialRepairs() + _vm.initialInsurance() + _vm.propertyManagementAmount());
        return _vm.totalInitialExpense();
    });

    _vm.calculatedRentToCoverMortgageAndExpenses = ko.computed(function () {
        return Math.round((_vm.totalInitialExpense() + _vm.annualInterestCost()) / (52 - _vm.initialVacancyRate()));
    });

    _vm.calculatedSurplusAfterExpenses = ko.computed(function () {
        return Math.round(_vm.initialRent() - _vm.calculatedRentToCoverMortgageAndExpenses());
    });

    _vm.processWrittenValueInt = function (value) {

        value = parseInt(value);

        return isNaN(value) ? 0 : value;
    };

    _vm.processWrittenValueFloat = function (value) {

        value = parseFloat(value);

        return isNaN(value) ? 0 : value;
    }

    _vm.UpdateTotalExpense = function () {
        return Math.round(_vm.initialRates() + _vm.initialRepairs() + _vm.initialInsurance() + _vm.propertyManagementAmount());
    }


    _vm.UpdateRent = function () {
        return Math.round(_vm.initialYieldPercentage() / 100 * _vm.price() / (52 - _vm.initialVacancyRate()));
    }

    _vm.UpdatePropertyManagement = function()
    {
        return Math.round(_vm.CalculateAnnualRent() * 8 / 100);
    }

    _vm.CalculateAnnualRent = function()
    {
        var calcAR = _vm.initialRent() * (52 - _vm.initialVacancyRate());
        return calcAR;
    }

    _vm.UpdateRentAndPropertyManagementCosts = function()
    {
        var calcRent = _vm.UpdateRent();
        _vm.initialRent(calcRent);

        var calcPMCost = _vm.UpdatePropertyManagement();
        _vm.propertyManagementAmount(calcPMCost);

        _vm.proposedAnnualRentalIncome(_vm.CalculateAnnualRent());
    }

    return _vm;
};