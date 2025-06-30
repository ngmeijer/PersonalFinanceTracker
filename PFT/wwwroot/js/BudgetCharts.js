const ctx = document.getElementById('myChart');
console.log(ctx);
var normalizedBudgets = [];

let currentChart;

function normalizeBudgetAmounts() {
    for (var index = 0; index < budgetValues.length; index++) {
        switch (budgetValues[index].interval) {
            //Weekly, * 52
            case 0:
                normalizedBudgets[index] = budgetAmounts[index] * 52;
                break;
            //Monthly, * 12
            case 1:
                normalizedBudgets[index] = budgetAmounts[index] * 12;
                break;
            //Yearly
            case 2:
                normalizedBudgets[index] = budgetAmounts[index];
                break;
        }
    }
}

normalizeBudgetAmounts();
createPieChart();

function createPieChart() {
    const data = {
        labels: budgetNames,
        datasets: [{
            label: 'Budgets',
            data: normalizedBudgets,
            hoverOffset: 4
        }]
    };

    var pieChart = new Chart(ctx, {
        type: 'pie',
        data: data,
        options: {
            plugins: {
                legend: {
                    display: true,
                    labels: {
                        color: 'rgb(255, 255, 255)'
                    }
                }
            }
        }
    });
}
