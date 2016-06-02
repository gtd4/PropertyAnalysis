﻿var PropertyDetailsViewModel = function (data) {
    var _vm = {};

    ko.mapping.fromJS(data, null, _vm);

    /*
    Price of Property. Editable
    */
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

    /*
    Property Yield Percentage. Editable
    */
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

    /*
    Amount of Rent to Achieve Yield Percentage. Editable
    */
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

    /*
    The Expected Vacancy rate in weeks. Editable
    */
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

    /*
    Possible Loan Interest Rates. Editable
    */
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
    
    /*
    The annual cost of interest on loan.
    */
    _vm.calculatedAnnualInterestAmount = ko.computed(function () {

        var annualInterest = Math.round(_vm.price() * _vm.initialInterestRate() / 100);
        _vm.annualInterestCost(annualInterest);

        return _vm.annualInterestCost();

    });

    /*
    How much rent needed to cover interest.
    */
    _vm.calculatedRentToCoverInterest = ko.computed(function () {
        return Math.round(_vm.annualInterestCost() / (52 - _vm.initialVacancyRate()));
    });

    /*
    Projected surplus from rent if Interest is covered.
    */
    _vm.calculateSurplusBeforeExpenses = ko.computed(function () {
        return Math.round(_vm.initialRent() - _vm.calculatedRentToCoverInterest());
    });

    /*
    Estimated Total of all rates on property. Editable
    */
    _vm.calculatedRates = ko.pureComputed(
        {
            read: function () {
                return _vm.initialRates();
            },
            write: function (value) {

                _vm.initialRates(_vm.processWrittenValueInt(value));

            }
        });

    /*
    Estimated amount to put away to cover maintenance
    */
    _vm.calculatedMaintenance = ko.pureComputed(
        {
            read: function () {
                return _vm.initialRepairs();
            },
            write: function (value) {
                _vm.initialRepairs(_vm.processWrittenValueInt(value));

            },
        });

    /*
    Possible Insurance Amount
    */
    _vm.calculatedInsurance = ko.pureComputed(
        {
            read: function () {
                return _vm.initialInsurance();
            },
            write: function (value) {
                _vm.initialInsurance(_vm.processWrittenValueInt(value));
            },
        });

    /*
    Estimated Cost to hire property managers
    */
    _vm.calculatedPropertyManagement = ko.pureComputed(
        {
            read: function () {

                return _vm.propertyManagementAmount();
            },
            write: function (value) {
                _vm.propertyManagementAmount(_vm.processWrittenValueInt(value));
            },
        });

    /*
    Total of all expenses (Rates, Maintenance, Insurance, Property Management)
    */
    _vm.calculatedTotalExpense = ko.computed(function () {

        _vm.totalInitialExpense(_vm.initialRates() + _vm.initialRepairs() + _vm.initialInsurance() + _vm.propertyManagementAmount());
        return _vm.totalInitialExpense();
    });

    /*
    Amount of rent needed to cover both the interest of the loan and the expenses
    */
    _vm.calculatedRentToCoverMortgageAndExpenses = ko.computed(function () {
        return Math.round((_vm.totalInitialExpense() + _vm.annualInterestCost()) / (52 - _vm.initialVacancyRate()));
    });

    /*
    Projected surplus left over from rent after all expenses have been paid
    */
    _vm.calculatedSurplusAfterExpenses = ko.computed(function () {
        return Math.round(_vm.initialRent() - _vm.calculatedRentToCoverMortgageAndExpenses());
    });

    /*
    Helper Method that Writes values as an int
    */
    _vm.processWrittenValueInt = function (value) {

        value = parseInt(value);

        return isNaN(value) ? 0 : value;
    };

    /*
    Helper Method that writes values as a float, used for our percentage values
    */
    _vm.processWrittenValueFloat = function (value) {

        value = parseFloat(value);

        return isNaN(value) ? 0 : value;
    }

    /*
    Calculates the expected rent value
    */
    _vm.UpdateRent = function () {
        return Math.round(_vm.initialYieldPercentage() / 100 * _vm.price() / (52 - _vm.initialVacancyRate()));
    }
    
    /*
    Calculates the expected Property Management Costs
    */
    _vm.UpdatePropertyManagement = function () {
        return Math.round(_vm.CalculateAnnualRent() * 8 / 100);
    }

    /*
    Calculates the annual rent amout
    */
    _vm.CalculateAnnualRent = function () {
        var calcAR = _vm.initialRent() * (52 - _vm.initialVacancyRate());
        return calcAR;
    }

    /*
    Updates Rent and Property Management Costs
    */
    _vm.UpdateRentAndPropertyManagementCosts = function () {
        var calcRent = _vm.UpdateRent();
        _vm.initialRent(calcRent);

        var calcPMCost = _vm.UpdatePropertyManagement();
        _vm.propertyManagementAmount(calcPMCost);

        _vm.proposedAnnualRentalIncome(_vm.CalculateAnnualRent());
    }

    return _vm;
};