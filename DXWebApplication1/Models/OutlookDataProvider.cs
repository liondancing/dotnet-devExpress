﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using DevExpress.Web.Mvc;
using DXWebApplication1.Controllers;

namespace DXWebApplication1.Models {
    // DXCOMMENT: File OutlookDataProvider.cs contains code that defines Scheduler's required helper and configures a data model common for all controls in the application 
    // DXCOMMENT: Configure a data model
    public static class OutlookDataProvider {
        const string OutlookDataContextKey = "DXOutlookDataContext";

        public static OutlookDataContext DB {
            get {
                if(HttpContext.Current.Items[OutlookDataContextKey] == null)
                    HttpContext.Current.Items[OutlookDataContextKey] = new OutlookDataContext();
                return (OutlookDataContext)HttpContext.Current.Items[OutlookDataContextKey];
            }
        }

        public static IEnumerable GetMessages() {
            return DB.Messages.ToList();
        }

        public static IEnumerable<Appointment> GetAppointments() {
            return DB.Appointments.ToList();
        }

        public static IEnumerable GetResources() {
            return DB.Resources.ToList();
        }

        public static Appointment GetEditableAppointment(int id) {
            return GetAppointments().FirstOrDefault(a => a.ID == id);
        }

        public static int GetNewAppointmentID() {
            IEnumerable<Appointment> appointments = GetAppointments();
            return (appointments.Count() > 0) ? appointments.Last().ID + 1 : 0;
        }

        public static void InsertAppointment(Appointment appointment) {
            appointment.ID = GetNewAppointmentID();
            DB.Appointments.Add(appointment);
            DB.SaveChanges();
        }

        public static void UpdateAppointment(Appointment appointment) {
            Appointment editableAppointment = GetEditableAppointment(appointment.ID);

            editableAppointment.AllDay = appointment.AllDay;
            editableAppointment.ContactInfo = appointment.ContactInfo;
            editableAppointment.Description = appointment.Description;
            editableAppointment.EndDate = appointment.EndDate;
            editableAppointment.EventType = appointment.EventType;
            editableAppointment.Label = appointment.Label;
            editableAppointment.Location = appointment.Location;
            editableAppointment.RecurrenceInfo = appointment.RecurrenceInfo;
            editableAppointment.ResourceID = appointment.ResourceID;
            editableAppointment.StartDate = appointment.StartDate;
            editableAppointment.Status = appointment.Status;
            editableAppointment.Subject = appointment.Subject;
            editableAppointment.ID = appointment.ID;

            DB.SaveChanges();
        }

        public static void DeleteAppointment(Appointment appointment) {
            Appointment editableAppointment = GetEditableAppointment(appointment.ID);
            if(editableAppointment != null) {
                DB.Appointments.Remove(editableAppointment);
                DB.SaveChanges();
            }
        }
    }
    
    public static class OutlookSchedulerHelper {
        static IEnumerable<Appointment> Appointments { 
            get { return OutlookDataProvider.GetAppointments(); } 
        }
        static IEnumerable Resources { 
            get { return OutlookDataProvider.GetResources(); } 
        }
        static MVCxAppointmentStorage AppointmentStorage = SchedulerHelper.Settings.Storage.Appointments;
        static MVCxResourceStorage ResourceStorage = SchedulerHelper.Settings.Storage.Resources;

        public static void UpdateModel() {
            InsertAppointment();
            UpdateAppointments();
            DeleteAppointments();
        }

        static void InsertAppointment() {
            Appointment appointment = SchedulerExtension.GetAppointmentToInsert<Appointment>("Scheduler", 
                OutlookDataProvider.GetAppointments(),
                OutlookDataProvider.GetResources(),
                SchedulerHelper.Settings.Storage.Appointments,
                SchedulerHelper.Settings.Storage.Resources
            );
            if(appointment != null)
                OutlookDataProvider.InsertAppointment(appointment);
        }
        static void UpdateAppointments() {
            foreach(Appointment appointment in SchedulerExtension.GetAppointmentsToUpdate<Appointment>("Scheduler", Appointments, Resources, AppointmentStorage, ResourceStorage))
                OutlookDataProvider.UpdateAppointment(appointment);
        }
        static void DeleteAppointments() {
            foreach(Appointment appointment in SchedulerExtension.GetAppointmentsToRemove<Appointment>("Scheduler", Appointments, Resources, AppointmentStorage, ResourceStorage))
                OutlookDataProvider.DeleteAppointment(appointment);
        }
    }

}