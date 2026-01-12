async function changeVisibility(elem, serviceId) {
    const resp = await fetch('/changeVisibility', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            serviceId: serviceId
        })
    })

    if (resp.ok) {
        console.log(resp.status)
        const body = await resp.json()
        const image = elem.querySelector('img')
        const card = elem.parentNode.parentNode
        if (body.visible) {
            image.src = '/img/visible.png'
            card.classList.remove('unvis-card')
        } else {
            image.src = '/img/unvisible.png'
            card.classList.add('unvis-card')
        }
    }
}

async function deleteService(elem, serviceId) {
    const userConfirm = confirm('Вы точно хотите удалить эту услугу?')
    if (!userConfirm) return

    const resp = await fetch('/deleteService', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            serviceId: serviceId
        })
    })

    if (resp.ok) {
        const card = elem.parentNode.parentNode
        card.parentNode.removeChild(card)
    }
}