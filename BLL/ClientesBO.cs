﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using pjPalmera.Entities;
using pjPalmera.DAL;

namespace pjPalmera.BLL
{
    public class ClientesBO
    {
        /// <summary>
        /// Messeger to Result from CustomerBO
        /// </summary>
        public static string strMensajeBO;

        /// <summary>
        /// Save customer's informations
        /// </summary>
        /// <param name="costumer"></param>
        /// <returns></returns>
        public static void Save(ClientesEntity costumer)
        {
            try
            {
                if (costumer == null)
                {
                    return;
                    throw new AggregateException("Verificar la información suministrada e intentar nuevamente.");
                }
                else
                {
                    var result = ClientesDAL.Create(costumer);

                    if (result == 1)
                    {
                        throw new ApplicationException("Salvado Satisfactoriamente.!!");
                        // strMensajeBO = "Actualizado Satisfactoriamente.!!";
                    }
                    else 
                    {
                        return;
                        throw new AggregateException("Uff, algo salio ocurrio y no se salvo.!!");
                    }
                }
            }

             catch (AggregateException aex)
            {
                MessageBox.Show("Mensaje del Sistema", aex.Message, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            catch (ApplicationException ae)
            {
                // strMensajeBO = ae.Message;
                MessageBox.Show("Mensaje del Sistema", ae.Message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            catch (Exception ex)
            {
                MessageBox.Show("Mensaje del Sistema", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        ///  Message about request create customer state
        /// </summary>
        /// <param name="cedula"></param>
        /// <returns>1 if</returns>
        public static bool ResultRequest(string cedula)
        {

            try
            {
                var status_request = ClientesDAL.ExitsCedula(cedula);

                if (status_request == false)
                {
                    return false;
                    throw new AggregateException("Cliente Registrado Satisfactoriamente!");
                    //strMensajeBO = "Cliente Registrado Satisfactoriamente!";
                }
                else if (status_request == true)
                {
                    return true;
                    throw new ApplicationException("Uppp Algo salio mal no se pudo realizar la operacion solicitada." + "\n Posiblemente se registro anteriormente un registro con las mismas informaciones indicadas. ");
                    // strMensajeBO = "No se puedo procesar el Registro del Nuevo Cliente";
                }
                return status_request;
            }

            catch (AggregateException ae)
            {
                strMensajeBO = ae.Message;
                return false;
            }

            catch (ApplicationException ae)
            {
                strMensajeBO = ae.Message;
                return true;

            }
            catch (Exception ex)
            {
                strMensajeBO = ex.Message;
                return true;
            }
        }

        /// <summary>
        /// Update customer information        --------------------------------> this need to create any exceptions for throw error without app
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static void Update(ClientesEntity customer)
        {
            var id = customer.Id;
            var name = customer.Nombre;
            //var MessengerDAL = "";
            try
            {
                if ((id == 1) || (name == "contado") || (name == "CONTADO") || (name == "Contado"))
                {
                    throw new ArgumentException("No es Posible Editar este Registro. Para cualquier asistencia contactar Soporte Técnico.");
                    // strMensajeBO = "No es Posible Editar este Registro. Para cualquier asistencia contactar Soporte Técnico.";
                    //return null
                }
                else if (id != 1)
                {
                    var val = ClientesDAL.Update(customer);

                    if (val == 1)
                    {
                        throw new ApplicationException("Se Editó Satisfactoriamente el Registro Indicado!!");
                        //strMensajeBO = "Se Editó Satisfactoriamente el Registro Indicado!!";
                    }
                    else 
                    {
                        throw new AggregateException("Uff Algo Salio mal..!!");
                        //strMensajeBO = "Se Editó Satisfactoriamente el Registro Indicado!!";
                    }
                }
            }

            catch (ArgumentException ape)
            {
                // strMensajeBO = ape.Message;
                MessageBox.Show(ape.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            catch (ApplicationException ae)
            {
                MessageBox.Show(ae.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            catch (AggregateException ae1)
            {
                MessageBox.Show(ae1.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        ///Delete Client by Id 
        /// </summary>
        /// <param name="id"></param>
        public static void Delete(long id)
        {
            try
            {
                if (id == 1)
                {
                    // strMensajeBO = "No es posible eliminar este Registro. Para cualquier asistencia contactar Soporte Técnico.";
                    //return;
                    throw new ApplicationException("No es posible eliminar este Registro. Para cualquier asistencia contactar Soporte Técnico.");
                }
                else if (id != 1)
                {

                    ClientesDAL.Delete(id);
                    throw new AggregateException("Eliminó Satisfactoriamente el Registro Indicado!!");
                    // strMensajeBO = "Eliminó Satisfactoriamente el Registro Indicado!!";
                }
            }

            catch (AggregateException ex)
            {
                strMensajeBO = ex.Message;
            }

            catch (ApplicationException ex)
            {
                strMensajeBO = ex.Message;
            }

            catch (Exception ex)
            {
                strMensajeBO = "Ocurrio un error " + ex + " no se elimino el cliente.";
                //return;

            }
        }

        /// <summary>
        ///Get all Costumers 
        /// </summary>
        /// <returns></returns>
        public static List<ClientesEntity> GetAll()
        {
            try
            {
                return ClientesDAL.All;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Get Users by Id from Customers
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static ClientesEntity GetbyId(long Id)
        {
            try
            {
              var customer =  ClientesDAL.GetbyId(Id);

                if(customer == null)
                {
                    throw new ApplicationException("La informaciones solicitadas fueron encontradas satisfactoriamente!!!");
                }
                else
                {
                    return customer;
                    throw new Exception("Algo salio mal o no existen informaciones asociadas con el id" + " " + customer.Id + " " + "que fue indicado.");
                }
            }

            catch (ApplicationException ax)
            {
                MessageBox.Show(ax.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
        }

        /// <summary>
        /// Get User by Id for Update Information
        /// </summary>
        /// <returns></returns>
        public static ClientesEntity GetbyCodeUpdate(long Id)
        {
            try
            {
                return ClientesDAL.GetbyCodeUpdate(Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        /// <summary>
        ///  Get Costumer by Lastname (List)
        /// </summary>
        /// <returns></returns>
        public static List<ClientesEntity> GetbyApellidos(string lastname)
        {
            try
            {
                return ClientesDAL.GetbyApellidos(lastname);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

        }

        /// <summary>
        ///  Get Costumer by Firstname (List)
        /// </summary>
        /// <returns></returns>
        public static List<ClientesEntity> GetbyNombre(string fistname)
        {
            try
            {
                var MessengerDAL = ClientesDAL.GetbyNombre(fistname);

                if (MessengerDAL != null)
                {
                    //strMensajeBO = "";
                    return ClientesDAL.GetbyNombre(fistname);
                }
                else
                {
                    strMensajeBO = "No se encontraron Nombres que concuerden con el/los indicados";
                    return ClientesDAL.GetbyNombre(fistname);
                }
            }
            catch (Exception ex)
            {
                strMensajeBO = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Filter Customer by cedula
        /// </summary>
        /// <param name="cedula"></param>
        /// <returns></returns>
        public static List<ClientesEntity> GetbyCedula(string cedula)
        {
            try
            {
                return ClientesDAL.GetbyCedula(cedula);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        /// <summary>
        /// Check if exits customer cedula
        /// </summary>
        /// <param name="cedula"></param>
        /// <returns></returns>
        public static bool ExitsCedula(string cedula)
        {

            try
            {
                var messengerDAL = ClientesDAL.ExitsCedula(cedula);

                if (messengerDAL == true)
                {
                    return true;
                    throw new AggregateException("Se encontro un registro asociado con este número de Cédula.");

                    //strMensajeBO = ";
                }
                else if (messengerDAL == false)
                {
                    return false;
                    throw new ApplicationException("No se encuentra registrado asociado a este número Cédula indicada. \n En cualquier caso verficar e intentar nuevamente.");

                    // strMensajeBO = ;
                }

                return messengerDAL;

            }

            catch (AggregateException age)
            {
                strMensajeBO = age.Message;
                return true;
            }

            catch (ApplicationException ape)
            {
                strMensajeBO = ape.Message;
                return false;
            }

            catch (Exception gex)
            {
                strMensajeBO = gex.Message;
                return false;
            }

        }


        /// <summary>
        /// Verify Current  Customer Amount in the checkout if it is posible
        /// </summary>
        /// <returns></returns>
        public static bool VeficafyCreditAmount(long idcustomer)
        {
            try
            {
                var getLineCreditAmountCustomer = ClientesDAL.Get_setCustomerAmount(idcustomer); // Get amount that set first time when registed  the Customer
                var getCurrentAmountCredit = ClientesDAL.Get_CrAmountCustomer(idcustomer);    //  Get total current from credit account manager

                if (getCurrentAmountCredit <= getLineCreditAmountCustomer)
                {
                   // return true;
                    throw new ApplicationException ("Disponible para comprar");
                    
                }
                else
                {
                   // return false;
                    throw new AggregateException ("No dispone de balance suficiente en su linea de crédito. Si requiere Solicitar un Supervisor.");
                }
            }
            catch(ApplicationException ae)
            {
                strMensajeBO = ae.Message;
               return true;
            }

            catch(AggregateException ae1)
            {
                strMensajeBO = ae1.Message;
                return false;
            }

            catch (Exception ex)
            {
                strMensajeBO = ex.Message;
                return true;
            }
        }

        /// <summary>
        /// Filter Customer by Cedula 
        /// </summary>
        /// <returns></returns>
        public static List<ClientesEntity> GetCustomerByCedula(string cedula)
        {
            try
            {
                return ClientesDAL.GetCustomerByCedula(cedula);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }


        /// <summary>
        /// Check if Exits Customer Code
        /// </summary>
        /// <returns></returns>
        public static bool ExitsCode(int code)
        {
            try 
            { 
                var messengerDAL = ClientesDAL.ExitsCode(code);

                if (messengerDAL == true)
                {
                    return true;
                    throw new ApplicationException("Se encontro el Código.");
                    // strMensajeBO = "Se encontro el Código";
                    
                }
                else if (messengerDAL == false)
                {
                    return false;
                    throw new AggregateException("No se encontro el Código indicado, verificar e intentar nuevamente.");
                  //  strMensajeBO = "No se encontro el Código indicado, verificar e intentar nuevamente";
                }

                return messengerDAL;
            }

            catch(ApplicationException ape)
            {
                strMensajeBO = ape.Message;
                return true;
            }

            catch(AggregateException age)
            {
                strMensajeBO = age.Message;
                return false;
            }

            catch(Exception ex)
            {
                strMensajeBO = ex.Message;
                return false;
            }

        }

    }

}