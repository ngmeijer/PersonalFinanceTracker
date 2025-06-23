function getPageContent() {
    return document.querySelector('.content');
}

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
document.querySelectorAll('[id$="-investment-button"').forEach(button => {
    button.addEventListener('click', () => {
        const modalType = button.id.split('-')[0];
        const modal = document.querySelector(`.investment-action-modal[data-modal="${modalType}"]`);
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
                handleRemoveInvestment();
                break;
            case 'change':
                handleChangeInvestment();
                break;
        }
    })
});

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
        url: '@Url.Action("AddInvestment", "Investments")',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(requiredData),
        success: function (response) {
            console.log('Success:', response)
            investmentModal.style.display = "none";
            getPageContent().classList.remove('blur');

            errorText.style.display = 'none';
            errorText.innerHTML = "";
        },
        error: function (response) {
            errorText.style.display = 'block';
            var errorMessage = "An error occured. Please check if the symbol: ";
            console.log("Error text reference: ", errorText);
            errorText.innerHTML = 'An error occurred. Please check if the symbol: <span id="error-message-symbol"></span> is valid.';
            const symbolSpan = document.getElementById('error-message-symbol');
            if (symbolSpan) {
                symbolSpan.textContent = givenSymbol;
                symbolSpan.style.color = "red";
            }
            console.log('Failure:', response, " - provided data:", requiredData);
        }
    });
}

$(".investment-table tr").click(function () {
    $(".investment-table").not($(this).closest("table")).find("tr").removeClass("selected-investment");
    $(this).addClass('selected-investment').siblings().removeClass('selected-investment');
    var value = $(this).find('td:first').html();
});