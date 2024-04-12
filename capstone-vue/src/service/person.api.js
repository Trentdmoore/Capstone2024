import axios from 'axios'

const BASE_URL = 'https://localhost:7238'

export const personApi = {
    async getAllPersons() {
       return await axios.get(`${BASE_URL}/allPersons`).then(response => {
            return response.data
        })
    }
}
personApi
