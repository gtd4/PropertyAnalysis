define(
[
'knockout',
'koMapping'
],
function (ko, koMap) {
    var PropertyDetailsViewModel = function (params) {
        var _vm = {};

        //ko.mapping = koMapping;
        koMap.fromJS(params.data, null, _vm);

        /*
        Price of Property. Editable
        */
        _vm.calculatedPrice = ko.pureComputed(
            {
                read: function () {
                    return _vm.price();
                },
                write: function (value) {

                    _vm.price(value);
                    _vm.price(_vm.processWrittenValueInt(value));

                    _vm.updateRentAndPropertyManagementCosts();
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
                _vm.initialYieldPercentage(value);
                _vm.initialYieldPercentage(_vm.processWrittenValueFloat(value));

                _vm.updateRentAndPropertyManagementCosts();
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
                _vm.initialRent(value);
                _vm.initialRent(_vm.processWrittenValueInt(value));
                var calcYield = (_vm.initialRent() * (52 - _vm.initialVacancyRate()) / _vm.price() * 100).toFixed(2);
                _vm.initialYieldPercentage(calcYield);

                var calcPMCost = _vm.updatePropertyManagement();
                _vm.propertyManagementAmount(calcPMCost);

                _vm.proposedAnnualRentalIncome(_vm.calculateAnnualRent());
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
                       _vm.initialVacancyRate(value)
                       _vm.initialVacancyRate(_vm.processWrittenValueInt(value));

                       _vm.updateRentAndPropertyManagementCosts();
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
                    _vm.initialInterestRate(value);
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
                    _vm.initialRates(value);
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
                    _vm.initialRepairs(value);
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
                    _vm.initialInsurance(value);
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
                    _vm.propertyManagementAmount(value);
                    _vm.propertyManagementAmount(_vm.processWrittenValueInt(value));
                },
            });

        _vm.calculatedBodyCorpFee = ko.pureComputed(
            {
                read: function () {

                    return _vm.bodyCorpFee();
                },
                write: function (value) {
                    _vm.bodyCorpFee(value);
                    _vm.bodyCorpFee(_vm.processWrittenValueInt(value));
                },
            });

        /*
        Total of all expenses (Rates, Maintenance, Insurance, Property Management)
        */
        _vm.calculatedTotalExpense = ko.computed(function () {

            _vm.totalInitialExpense(_vm.initialRates() + _vm.initialRepairs() + _vm.initialInsurance() + _vm.propertyManagementAmount() + _vm.bodyCorpFee());
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

        _vm.calculateAnnualEarnings = ko.computed(function () {

            return _vm.calculatedSurplusAfterExpenses() * (52 - _vm.initialVacancyRate());
        });

        /*
        Helper Method that Writes values as an int
        */
        _vm.processWrittenValueInt = function (value) {

            value = parseInt(value, 10);

            return isNaN(value) ? 0 : value;
        };

        /*
        Helper Method that writes values as a float, used for our percentage values
        */
        _vm.processWrittenValueFloat = function (value) {

            value = value.replace(/[^0-9.]/g, "");

            var splitVal = value.split('.');

            //make sure there is only 1 decimal point
            if (splitVal.length > 2)
            {
                value = splitVal[0] + "." + splitVal[1];
            }

            return isNaN(value) ? 0 : value;
        }

        /*
        Calculates the expected rent value
        */
        _vm.updateRent = function () {
            return Math.round(_vm.initialYieldPercentage() / 100 * _vm.price() / (52 - _vm.initialVacancyRate()));
        }

        /*
        Calculates the expected Property Management Costs
        */
        _vm.updatePropertyManagement = function () {
            return Math.round(_vm.calculateAnnualRent() * 8 / 100);
        }

        /*
        Calculates the annual rent amout
        */
        _vm.calculateAnnualRent = function () {
            var calcAR = _vm.initialRent() * (52 - _vm.initialVacancyRate());
            return calcAR;
        }

        /*
        Updates Rent and Property Management Costs
        */
        _vm.updateRentAndPropertyManagementCosts = function () {
            var calcRent = _vm.updateRent();
            _vm.initialRent(calcRent);

            var calcPMCost = _vm.updatePropertyManagement();
            _vm.propertyManagementAmount(calcPMCost);

            _vm.proposedAnnualRentalIncome(_vm.calculateAnnualRent());
        }

        return _vm;
    };

    return PropertyDetailsViewModel;
});





