import axios from 'axios'

const BASE_URL = 'https://localhost:7238'

export const personApi = {
    async getTestingInfo() {
        await axios.get(`${BASE_URL}/testingInfo`).then(response => {
            return response.data
        })
    }
}
personApi
