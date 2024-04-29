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
    },
    async getAllAccessLogs() {
        return await axios.get(`${BASE_URL}/AllAccessLogs`).then(response => {
            return response.data
        })
    },
    async getAllRequestLogs() {
        return await axios.get(`${BASE_URL}/AllRequestLogs`).then(response => {
            return response.data
        })
    },
    async updatePersonInfo(personSelectedObj) {
        return await axios({
            method: 'put',
            url: `${BASE_URL}/UpdatePerson`,
            data: personSelectedObj
        }).then(response => {
            return response.data
        }).catch(e => {
            console.log(e);
        })
    },
    async deletePerson(personSelectedObjID) {
       return await axios({
        method: 'delete',
        url:    `${BASE_URL}/DeletePerson/` + personSelectedObjID,
        data:   personSelectedObjID
        }).then(response => {
        return response.data
        }).catch(e => {
        console.log(e);
        })
    }
}

personApi
