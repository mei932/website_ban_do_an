var plusBtn = document.querySelector(".plus");
var minusBtn = document.querySelector(".minus");
var quantity = document.getElementById("quantity_value");

let counter = 1;

plusBtn.addEventListener("click", () => {
    counter++;
    updateDisplay();
})

minusBtn.addEventListener("click", () => {
    if (counter > 1) {
        counter--;
    }
    updateDisplay();
})

function updateDisplay() {
    quantity.innerHTML = counter;
}

