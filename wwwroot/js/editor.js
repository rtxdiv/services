const imageInp = document.querySelector('#imageInp')
const image = document.querySelector('#image')
const imgbox = document.querySelector('.imgbox')
const nameInp = document.querySelector('#nameInp')
const descriptionInp = document.querySelector('#descriptionInp')
const requirementsInp = document.querySelector('#requirementsInp')
const serviceId = document.querySelector('#serviceId')

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
            image.classList.remove('no-image')
        }
        reader.readAsDataURL(file)
    }
}

async function createService() {
    if (checkValues()) return

    const formdata = new FormData();
    formdata.append('Image', imageInp.files[0])
    formdata.append('Name', nameInp.value)
    formdata.append('Description', descriptionInp.value)
    formdata.append('Requirements', requirementsInp.value)

    const resp = await fetch('/create', {
        method: 'POST',
        body: formdata
    })

    if (resp.redirected) {
        window.location.href = resp.url
    }

    if (!resp.ok) {
        if (resp.status == 400) {
            const body = await resp.json()
            return displayErrors(body)
        }
        document.querySelector(`[data-error="All"]`).textContent = 'Ошибка сервера'
    } else {
        console.log(resp)
    }
}

async function updateService() {
    if (checkValues()) return

    const formdata = new FormData();
    formdata.append('Image', imageInp.files[0])
    formdata.append('Name', nameInp.value)
    formdata.append('Description', descriptionInp.value)
    formdata.append('Requirements', requirementsInp.value)
    formdata.append('ServiceId', serviceId.value)

    const resp = await fetch('/update', {
        method: 'POST',
        body: formdata
    })

    if (resp.redirected) {
        window.location.href = resp.url
    }

    if (!resp.ok) {
        if (resp.status == 400) {
            const body = await resp.json()
            return displayErrors(body)
        }
        document.querySelector(`[data-error="All"]`).textContent = 'Ошибка сервера'
    } else {
        console.log(resp)
    }
}


function checkValues() {
    let error = false

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

let errorElems = []
function displayErrors(errors) {
    errorElems.forEach(elem => {
        elem.textContent = ''
    })
    errorElems = []
    Object.entries(errors).forEach(([key, value]) => {
        const elem = document.querySelector(`[data-error="${key}"]`)
        if (elem) {
            errorElems.push(elem)
            elem.textContent = value[0]
        }
    })
}