$("#refresh-investments-button").click(function () {
    const container = document.querySelector("#investments-tables-container");
    const refreshUrl = container.dataset.refreshUrl;

    $.ajax({
        url: refreshUrl,
        type: 'POST',
        success: function (html) {
            $(container).html(html);
        }
    });
});

//Enables the proper modal
const quantityField = document.getElementById('change-quantity');

document.querySelectorAll('[id$="-investment-button"').forEach(button => {
    button.addEventListener('click', () => {
        const modalType = button.id.split('-')[0];
        const modal = document.querySelector(`.investment-action-modal[data-modal="${modalType}"]`);
        if (modalType == "remove") {
            if (currentlySelectedStock == null)
                return;
        }

        if (modalType == "change") {
            quantityField.value = amountOfSelectedStock;
        }

        modal.style.display = "flex";
        getPageContent().classList.add('blur');
    });
});

//Closes the current modal
document.querySelectorAll('.cancel-button').forEach(button => {
    button.addEventListener('click', () => {
        const modalType = button.dataset.modal;
        const modal = document.querySelector(`.investment-action-modal[data-modal="${modalType}"]`);
        modal.style.display = "none";
        getPageContent().classList.remove('blur');

        const errorText = modal.querySelector('.error-content');
        if (errorText) {
            errorText.innerHTML = "";
            errorText.style.display = "none";
        }
    });
});

document.querySelectorAll('.confirm-button').forEach(button => {
    button.addEventListener('click', () => {
        const modalType = button.dataset.modal;

        switch (modalType) {
            case 'add':
                handleAddInvestment();
                break;
            case 'remove':
                if (currentlySelectedStock == null)
                    return;

                handleRemoveInvestment();
                break;
            case 'change':
                handleChangeInvestment();
                break;
        }
    })
});

const addInvestmentModal = document.querySelector(`.investment-action-modal[data-modal="add"]`);
const errorText = addInvestmentModal.querySelector('.error-content');
function handleAddInvestment() {
    var givenSymbol = $('#symbol').val();
    var givenQuantity = $('#quantity').val();
    var givenType = $('#investmentType').val();
    var requiredData = {
        Symbol: givenSymbol,
        Quantity: givenQuantity,
        Type: givenType
    };

    $.ajax({
        url: addInvestmentUrl,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(requiredData),
        success: function (response) {
            console.log('Success:', response)
            addInvestmentModal.style.display = "none";
            getPageContent().classList.remove('blur');

            if (errorText) {
                errorText.style.display = 'none';
                errorText.innerHTML = "";
            }
        },
        error: function (response) {
            if (errorText) {
                errorText.style.display = 'block';
                errorText.innerHTML = 'An error occurred. Please check if the symbol: <span id="error-message-symbol"></span> is valid.';
                const symbolSpan = document.getElementById('error-message-symbol');
                if (symbolSpan) {
                    symbolSpan.textContent = givenSymbol;
                    symbolSpan.style.color = "red";
                }
            }
            console.log('Failure:', response, " - provided data:", requiredData);
        }
    });
}

const removeInvestmentModal = document.querySelector(`.investment-action-modal[data-modal="remove"]`);

function handleRemoveInvestment() {
    $.ajax({
        url: removeInvestmentUrl,
        type: 'DELETE',
        contentType: 'application/json',
        data: JSON.stringify(currentlySelectedStock),
        success: function (response) {
            console.log('Success:', response)
            removeInvestmentModal.style.display = "none";
            getPageContent().classList.remove('blur');

            if (errorText) {
                errorText.style.display = 'none';
                errorText.innerHTML = "";
            }
        },
        error: function (response) {
            if (errorText) {
                errorText.style.display = 'block';
                errorText.innerHTML = 'An error occurred. Could not remove the selected investment with symbol:' + currentlySelectedStock;
                const symbolSpan = document.getElementById('error-message-symbol');
                if (symbolSpan) {
                    symbolSpan.textContent = givenSymbol;
                    symbolSpan.style.color = "red";
                }
            }
            console.log('Failure:', response, " - provided data:", currentlySelectedStock);
        }
    });
}

const changeInvestmentModal = document.querySelector(`.investment-action-modal[data-modal="change"]`);
function handleChangeInvestment() {
    var givenQuantity = $('#change-quantity').val();
    var requiredData = {
        Symbol: currentlySelectedStock,
        Quantity: givenQuantity,
    };
    $.ajax({
        url: changeInvestmentUrl,
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(requiredData),
        success: function (response) {
            console.log('Success:', response)
            addInvestmentModal.style.display = "none";
            getPageContent().classList.remove('blur');

            if (errorText) {
                errorText.style.display = 'none';
                errorText.innerHTML = "";
            }
        },
        error: function (response) {
            console.log('Failure:', response, " - provided data:", requiredData);
        }
    });
}

var currentlySelectedStock = null;
var amountOfSelectedStock = 0;
$(".stock-instance").click(function () {
    $(".stock-instance").not(this).removeClass("selected-investment");
    $(this).addClass('selected-investment');
    var symbol = $(this).find('td').eq(1).html();
    currentlySelectedStock = symbol;

    var amount = $(this).find('td').eq(2).html();
    amountOfSelectedStock = amount;
});