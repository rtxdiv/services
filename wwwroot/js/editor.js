const imageInp = document.querySelector('#imageInp')
const image = document.querySelector('#image')
const imgbox = document.querySelector('.imgbox')
const nameInp = document.querySelector('#nameInp')
const descriptionInp = document.querySelector('#descriptionInp')
const requirementsInp = document.querySelector('#requirementsInp')

function imgClick() {
    imageInp.click()
}
function imgChange() {
    const files = imageInp.files
    imgbox.classList.remove('image-error')

    if (files.length > 0) {

        const file = files[0]
        const reader = new FileReader()

        reader.onload = function (e) {
            image.src = e.target.result
        }
        reader.readAsDataURL(file)
    }
}

async function createService() {
    if (checkValues(true)) return

    console.log('/createServcie')
}

function updateService() {
    if (checkValues(false)) return

    console.log('/editService')
}


function checkValues(checkImage = true) {
    let error = false

    if (checkImage) {
        if (imageInp.files.length == 0) {
            imgbox.classList.add('image-error')
            error = true
        }
    }

    if (nameInp.value.length == 0) {
        nameInp.classList.add('input-error')
        error = true
    }
    if (descriptionInp.value.length == 0) {
        descriptionInp.classList.add('input-error')
        error = true
    }
    if (requirementsInp.value.length == 0) {
        requirementsInp.classList.add('input-error')
        error = true
    }
    return error
}