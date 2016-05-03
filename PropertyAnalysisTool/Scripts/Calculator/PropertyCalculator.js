function SetRent(parent) {
    var propVal = parent.find("#Price").val();
    var yieldVal = parent.find("#InitialYieldPercentage").val();
    var vacancyRate = parent.find("#InitialVacancyRate").val();

    var rent = Math.round(yieldVal / 100 * propVal / (52 - vacancyRate));

    parent.find("#InitialRent").val(rent);
}

function SetYield(parent) {
    var propVal = parent.find("#Price").val();
    var rent = parent.find("#InitialRent").val();
    var vacancyRate = parent.find("#InitialVacancyRate").val();

    var yieldVal = Math.round(rent * (52 - vacancyRate) / propVal * 100);
    parent.find("#InitialYieldPercentage").val(yieldVal);
}

function SetSurplus(parent) {
    var rent = parent.find("#InitialRent").val();
    var rentToCoverInterest = parent.find("#RentToCoverInterest").val();
    var rentoToCoverExpense = parent.find("#RentToCoverMortgageExpenses").val();

    var surplusBeforeExpense = Math.round(rent - rentToCoverInterest);
    var surplusAfterExpense = Math.round(rent - rentoToCoverExpense);

    parent.find("#SurplusBeforeExpenses").val(surplusBeforeExpense);
    parent.find("#SurplusAfterExpense").val(surplusAfterExpense);
}

function SetRentToCoverInterest(parent) {
    SetInterestPerAnnum(parent);
    var interestCost = parent.find("#AnnualInterestCost").val();
    var vacancy = parent.find("#InitialVacancyRate").val();

    var rentToCoverInterest = Math.round(interestCost / (52 - vacancy));

    parent.find("#RentToCoverInterest").val(rentToCoverInterest);
}

function SetInterestPerAnnum(parent) {
    var price = parent.find("#Price").val();
    var interest = parent.find("#InitialInterestRate").val();

    var annualInterest = Math.round(price * interest / 100);

    parent.find("#AnnualInterestCost").val(annualInterest);
}

function UpdateTotalExpenses(parent) {
    var rates = Number(parent.find("#InitialRates").val());
    var repairs = Number(parent.find("#InitialRepairs").val());
    var insurance = Number(parent.find("#InitialInsurance").val());
    var propertyManagement = Number(parent.find("#PropertyManagementAmount").val());
    var vacancyRate = parent.find("#InitialVacancyRate").val();
    var totalExpense = Math.round(rates + repairs + insurance + propertyManagement);
    var totalInterest = Number(parent.find("#AnnualInterestCost").val());

    var rentToCoverExpense = Math.round((totalExpense + totalInterest) / (52 - vacancyRate));
    parent.find("#RentToCoverMortgageExpenses").val(rentToCoverExpense);
    parent.find("#TotalInitalExpense").val(totalExpense);
}

function UpdateValues(parent) {
    SetRent(parent);
    SetInterestPerAnnum(parent);
    SetRentToCoverInterest(parent);
    UpdatePropertyManagementCosts(parent);
    UpdateTotalExpenses(parent);
    SetSurplus(parent);
}

function UpdatePropertyManagementCosts(parent) {
    var annualRent = Number(parent.find("#InitialRent").val() * (52 - parent.find("#InitialVacancyRate").val()));

    var pmCost = Math.round(annualRent * 8 / 100);

    parent.find("#PropertyManagementAmount").val(pmCost)
}

$(function () {
    $(".calculator-container").on("keyup", "#Price", function () {
        var parent = $(this).parents(".calculator-container");

        UpdateValues(parent);
    });

    $(".calculator-container").on("keyup", "#InitialYieldPercentage", function () {
        var parent = $(this).parents(".calculator-container");

        UpdateValues(parent);
    });

    $(".calculator-container").on("keyup", "#InitialVacancyRate", function () {
        var parent = $(this).parents(".calculator-container");

        UpdateValues(parent);
    });

    $(".calculator-container").on("keyup", "#InitialRent", function () {
        var parent = $(this).parents(".calculator-container");

        SetYield(parent);
        SetInterestPerAnnum(parent);
        SetRentToCoverInterest(parent);
        UpdatePropertyManagementCosts(parent);
        UpdateTotalExpenses(parent);
        SetSurplus(parent);
    });

    $(".calculator-container").on("keyup", "#InitialInterestRate", function () {
        var parent = $(this).parents(".calculator-container");

        UpdateValues(parent);
    });

    $(".calculator-container").on("keyup", "#InitialRates", function () {
        var parent = $(this).parents(".calculator-container");

        UpdateTotalExpenses(parent);
        SetSurplus(parent);
    });

    $(".calculator-container").on("keyup", "#InitialRepairs", function () {
        var parent = $(this).parents(".calculator-container");

        UpdateTotalExpenses(parent);
        SetSurplus(parent);
    });

    $(".calculator-container").on("keyup", "#InitialInsurance", function () {
        var parent = $(this).parents(".calculator-container");

        UpdateTotalExpenses(parent);
        SetSurplus(parent);
    });

    $(".calculator-container").on("keyup", "#PropertyManagementAmount", function () {
        var parent = $(this).parents(".calculator-container");

        UpdateTotalExpenses(parent);
        SetSurplus(parent);
    });
});