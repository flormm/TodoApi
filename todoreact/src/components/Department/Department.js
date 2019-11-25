import React from 'react';
import {connect} from 'react-redux';
import DepartmentService from '../../services/DepartmentService';
import DepartmentItem from './DepartmentItem/DepartmentItem';
import DepartmentAddItem from './DepartmentAddItem/DepartmentAddItem';

//import axios from 'axios';
import dotenv from 'dotenv';
dotenv.config();


class Department extends React.Component {
    
    constructor(props) {
        super(props);
        this.DepartmentService = new DepartmentService(); // Instancio el servicio
   
   
    }

    async componentDidMount() {
        var respuesta = await this.DepartmentService.getDepartamento(); // Uso el servicio instanciado
        this.props.onInit(respuesta);
    }



    render() {
        var listado = this.props.listado.map(unItem => {
           return <DepartmentItem item={unItem} key={unItem.id}/>
        })

        return (
          

           <>         
               <h1>Agregar nuevo departamento</h1>
               <DepartmentAddItem />

               {listado}
           </>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        listado: state.taskList,
    }
}

const mapActions = (dispatch) => {
    return {
        onInit: (listado) => dispatch({type: 'INIT', data: listado})
    }
}

const functionRespuesta = connect(mapStateToProps, mapActions);
export default functionRespuesta(Department);