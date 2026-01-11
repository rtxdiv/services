Array.from(document.querySelectorAll('.card')).forEach(card => {
    const dateElem = card.querySelector('.date')
    const date = new Date(dateElem.textContent + 'Z')
    dateElem.textContent = `${String(date.getDate()).padStart(2, '0')}/${String(date.getMonth() + 1).padStart(2, '0')}/${date.getFullYear()}`
})

async function accept(elem, id) {
    const resp = await fetch('/accept', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            requestId: id
        })
    })

    if (resp.status == 404) {
        return document.removeChi
    }

    if (resp.ok) {
        const body = await resp.json()
        const statusBlock = elem.parentNode.parentNode.querySelector('.status')
        statusBlock.textContent = body.text
        setButtons(elem.parentNode, body.status)

        if (body.status == false) {
            statusBlock.className = 'status'
            statusBlock.classList.add('rejected')
        } else if (body.status == true) {
            statusBlock.className = 'status'
            statusBlock.classList.add('accepted')
        } else {
            statusBlock.className = 'status'
            statusBlock.classList.add('waiting')
        }
    }
}

async function reject(elem, id) {
    const resp = await fetch('/reject', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            requestId: id
        })
    })

    if (resp.ok) {
        const body = await resp.json()
        const statusBlock = elem.parentNode.parentNode.querySelector('.status')
        statusBlock.textContent = body.text
        setButtons(elem.parentNode, body.status)

        if (body.status == false) {
            statusBlock.className = 'status'
            statusBlock.classList.add('rejected')
        } else if (body.status == true) {
            statusBlock.className = 'status'
            statusBlock.classList.add('accepted')
        } else {
            statusBlock.className = 'status'
            statusBlock.classList.add('waiting')
        }
    }
}

function setButtons(buttonsBlock, accepted) {
    const acceptBtn = buttonsBlock.querySelector('.acceptBtn')
    const rejectBtn = buttonsBlock.querySelector('.rejectBtn')

    if (accepted == true) {
        acceptBtn.className = 'acceptBtn'
        rejectBtn.className = 'rejectBtn visible'
    } else if (accepted == false) {
        acceptBtn.className = 'acceptBtn visible'
        rejectBtn.className = 'rejectBtn'
    } else {
        acceptBtn.className = 'acceptBtn visible'
        rejectBtn.className = 'rejectBtn visible'
    }
}