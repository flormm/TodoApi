import axios from 'axios';

export default class DepartmentService {
    
    constructor() {
        this.http = axios;
        this.serverUrl = process.env.REACT_APP_SERVER + '/Department/';
        this.serverUrl ='http://localhost:32936/api/Department/';


    }

    async getDepartamento() {
      const respuesta = await this.http.get(this.serverUrl);
      return respuesta.data;     


    };

   

    async postDepartamento(unDepart) {
        if (unDepart.name === "") {
            throw new Error('Debe ingresar un nombre para el departamento');
        }
        const respuesta = await this.http.post(this.serverUrl, unDepart);
        return respuesta.data;
    }

    async putDepart(unDepart) {
        if (unDepart.name === "") {
            throw new Error('Debe ingresar un nombre para el departamento');
        }
        const respuesta = await this.http.put(this.serverUrl + "/" + unDepart.id, unDepart);
        return respuesta.data;
    }

    async deleteDepart(id) {
        await this.http.delete(this.serverUrl + "/" + id);        
    }

}