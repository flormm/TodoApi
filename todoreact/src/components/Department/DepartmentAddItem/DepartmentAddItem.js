import React from 'react';
import {connect} from 'react-redux';
import DepartmentService from '../../../services/DepartmentService';

class DepartmentAddItem extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            zona: '',
            mensaje_error: ''
        }
        this.cambioInput = this.cambioInput.bind(this);
        this.save = this.save.bind(this);
        this.DepartmentService = new DepartmentService(); // Instancio el servicio
    }

    cambioInput(e) {
        if (e.target.name==="zona") {
            this.setState({zona: e.target.value});
        }
        
    }

    async save() {
        if (this.state.zona==='') {
            this.setState({mensaje_error: 'Debe cargar un Departamento '});
            return;
        }
        this.setState({mensaje_error: ''});
        const obj = {
            name: this.state.zona,


        }
        const respuesta = await this.DepartmentService.postDepartamento(obj);
        this.props.onSave(respuesta);
    }

    render() {

        const mensajeError = this.state.mensaje_error==='' ? null : <div className="alert alert-danger">{this.state.mensaje_error}</div>;

        return (
            <div>{mensajeError}
                <div className="input-group mb-3 todo-add">
                    <input type="text" className="form-control" name="zona" placeholder="Nuevo departamento" value={this.state.zona} onChange={this.cambioInput}/>
                    <div className="input-group-append">
                        <button className="btn btn-outline-primary" type="button" onClick={this.save}><i className="fa fa-plus"></i></button>
                    </div>
                </div>
            </div>
        )
    }
}


const mapDispatch = (dispatch) => {
    return {
        onSave: (obj) => {dispatch({type:'ADD_ITEM', data: obj})}
    }
}

export default connect(mapDispatch)(DepartmentAddItem);