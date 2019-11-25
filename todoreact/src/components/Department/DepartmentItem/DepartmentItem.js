import React from 'react';
import {connect} from 'react-redux';
import DepartmentService from '../../../services/DepartmentService';

class DepartmentItem extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            en_edicion: false,
            zona: '',
 
        }
        this.habilitarEdicion = this.habilitarEdicion.bind(this);
        this.cambioInput = this.cambioInput.bind(this);
        this.save = this.save.bind(this);
        this.cancel = this.cancel.bind(this);
        this.toggleDone = this.toggleDone.bind(this);
        this.DepartmentService = new DepartmentService(); // Instancio el servicio

    }

    habilitarEdicion(e) {
        this.setState({
            en_edicion: true,
            zona: this.props.item.name,
           
        });
        e.preventDefault();
    }

    cambioInput(e) {
        if (e.target.name==="zona") {
            this.setState({zona: e.target.value});
        }

    }    

    async save() {
        const obj = {
            id: this.props.item.id,
            name: this.state.Zona,

        }
        await this.DepartmentService.putZona(obj);
        this.props.onUpdate(obj);
        this.setState({
            en_edicion: false,
            zona: '',
            
        });        
    }


    async toggleDone() {
        const obj = {
            id: this.props.item.id,
            name: this.props.item.name,

        }
        await this.DepartmentService.putZona(obj);
        this.props.onUpdate(obj);    
    }    

    cancel() {
        this.setState({
            en_edicion: false,
            zona: '',

        });
    }



    render() {
        var contenidoTarjeta;
        if (this.props.item.isComplete) {
            contenidoTarjeta = <>
                                <button className="btn btn-success" onClick={this.toggleDone}><i className="fa fa-check"></i></button> &nbsp;
                                {this.props.item.name}
                                </>
        } else {
            contenidoTarjeta = <>
                                <button className="btn btn-outline-secondary" onClick={this.toggleDone}><i className="fa fa-check"></i></button> &nbsp;
                                {this.props.item.name}
                                </>
        }

        const listadoResponsables = this.props.responsables.map(unItem => {
            return <option value={unItem.id} key={unItem.id}>{unItem.nombre}</option>
        })        
        if (this.state.en_edicion) {
            contenidoTarjeta = 
                                <div className="input-group mb-3 todo-item">
                                    <input type="text" className="form-control" name="zona" placeholder="Nuevo departamento" value={this.state.zona} onChange={this.cambioInput}/>
                                    <select name="responsable" className="custom-select" value={this.state.responsable_seleccionado_id} onChange={this.cambioInput}>
                                        {listadoResponsables}
                                    </select>
                                    <div className="input-group-append">
                                        <button className="btn btn-outline-success" type="button" onClick={this.save}><i className="fa fa-check"></i></button>
                                        <button className="btn btn-outline-danger" type="button" onClick={this.cancel}><i className="fa fa-times"></i></button>
                                    </div>
                                </div>
                                
        }
        return (
            <div className="card">
                <div className="card-body">
                    <h5 className="card-title" onDoubleClick={this.habilitarEdicion}>{contenidoTarjeta}</h5>
                </div>
                
            </div>
        )
    }
}


const mapDispatch = (dispatch) => {
    return {
        onUpdate: (obj) => {dispatch({type:'UPDATE_ITEM', data: obj})}
    }
}

export default connect(mapDispatch)(DepartmentItem);