import axios from 'axios'

const BASE_URL = 'https://localhost:7238'

export const personApi = {
    async getAllPersons() {
       return await axios.get(`${BASE_URL}/allPersons`).then(response => {
            return response.data
        })
    },
    async insertPersonInfo(personObj) {
        return await axios({
            method: 'post',
            url: `${BASE_URL}/InsertPerson`,
            data: personObj
        }).then(response => {
            return response.data         
        }).catch(e => {
            console.log(e);
        })
    }
}
personApi
