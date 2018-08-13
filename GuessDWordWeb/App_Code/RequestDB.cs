using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// This class contains all the database code for RequestDB
/// </summary>
public class RequestDB
{
    #region Public Methods

    // Returns an item from the Request table
    public static Request GetItem(int RequestID)
    {
        Request myRequest = null;
        using (SqlConnection myConnection = new SqlConnection(AppConfiguration.SqlConnectionString))
        {
            using (SqlCommand myCommand = new SqlCommand("sprocRequestSelectItem", myConnection))
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@RequestID", RequestID);
                myConnection.Open();
                using (SqlDataReader myReader = myCommand.ExecuteReader())
                {
                    if (myReader.Read())
                    {
                        myRequest = FillDataRecord(myReader);
                    }
                    myReader.Close();
                }
            }
            myConnection.Close();
        }
        return myRequest;
    }

    // Returns all items in the Request table and fill it in a RequestList
    public static RequestList GetList()
    {
        RequestList tempList = new RequestList();
        using (SqlConnection myConnection = new SqlConnection(AppConfiguration.SqlConnectionString))
        {
            using (SqlCommand myCommand = new SqlCommand("sprocRequestSelectList", myConnection))
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myConnection.Open();
                using (SqlDataReader myReader = myCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        tempList = new RequestList();
                        while (myReader.Read())
                        {
                            tempList.Add(FillDataRecord(myReader));
                        }
                    }
                    myReader.Close();
                }
            }
        }
        return tempList;
    }

    // Returns items in the User table based on search criteria and fill it in a UserList
    public static RequestList GetList(RequestSearchCriteria requestSearchCriteria)
    {
        RequestList tempList = new RequestList();
        using (SqlConnection myConnection = new SqlConnection(AppConfiguration.SqlConnectionString))
        {
            using (SqlCommand myCommand = new SqlCommand("sprocRequestSearchList", myConnection))
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(requestSearchCriteria.CreatedBy))
                {
                    myCommand.Parameters.AddWithValue("@CreatedBy", requestSearchCriteria.CreatedBy);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.ClosedBy))
                {
                    myCommand.Parameters.AddWithValue("@ClosedBy", requestSearchCriteria.ClosedBy);
                }
                if (requestSearchCriteria.CreateDateFrom != DateTime.MinValue)
                {
                    myCommand.Parameters.AddWithValue("@CreateDateFrom", requestSearchCriteria.CreateDateFrom);
                }
                if (requestSearchCriteria.CreateDateTo != DateTime.MinValue)
                {
                    myCommand.Parameters.AddWithValue("@CreateDateTo", requestSearchCriteria.CreateDateTo);
                }
                if (requestSearchCriteria.CloseDateFrom != DateTime.MinValue)
                {
                    myCommand.Parameters.AddWithValue("@CloseDateFrom", requestSearchCriteria.CloseDateFrom);
                }
                if (requestSearchCriteria.CloseDateTo != DateTime.MinValue)
                {
                    myCommand.Parameters.AddWithValue("@CloseDateTo", requestSearchCriteria.CloseDateTo);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.AssignedUser))
                {
                    myCommand.Parameters.AddWithValue("@AssignedUser", requestSearchCriteria.AssignedUser);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.AssignedDepartment))
                {
                    myCommand.Parameters.AddWithValue("@AssignedDepartment", requestSearchCriteria.AssignedDepartment);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.RequestStatus))
                {
                    myCommand.Parameters.AddWithValue("@RequestStatus", requestSearchCriteria.RequestStatus);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.CaseName))
                {
                    myCommand.Parameters.AddWithValue("@CaseName", requestSearchCriteria.CaseName);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.TicketNumber))
                {
                    myCommand.Parameters.AddWithValue("@TicketNumber", requestSearchCriteria.TicketNumber);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.PNR))
                {
                    myCommand.Parameters.AddWithValue("@PNR", requestSearchCriteria.PNR);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.FlightNumber))
                {
                    myCommand.Parameters.AddWithValue("@FlightNumber", requestSearchCriteria.FlightNumber);
                }
                if (requestSearchCriteria.FlightDate != DateTime.MinValue)
                {
                    myCommand.Parameters.AddWithValue("@FlightDate", requestSearchCriteria.FlightDate);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.DepartureSector))
                {
                    myCommand.Parameters.AddWithValue("@DepartureSector", requestSearchCriteria.DepartureSector);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.ArrivalSector))
                {
                    myCommand.Parameters.AddWithValue("@ArrivalSector", requestSearchCriteria.ArrivalSector);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.TicketCabin))
                {
                    myCommand.Parameters.AddWithValue("@TicketCabin", requestSearchCriteria.TicketCabin);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.TravelCabin))
                {
                    myCommand.Parameters.AddWithValue("@TravelCabin", requestSearchCriteria.TravelCabin);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.Justification))
                {
                    myCommand.Parameters.AddWithValue("@Justification", requestSearchCriteria.Justification);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.Mobile))
                {
                    myCommand.Parameters.AddWithValue("@PassengerMobile", requestSearchCriteria.Mobile);
                }
                if (!string.IsNullOrEmpty(requestSearchCriteria.Email))
                {
                    myCommand.Parameters.AddWithValue("@PassengerEmail", requestSearchCriteria.Email);
                }
                myConnection.Open();
                using (SqlDataReader myReader = myCommand.ExecuteReader())
                {
                    if (myReader.HasRows)
                    {
                        tempList = new RequestList();
                        while (myReader.Read())
                        {
                            tempList.Add(FillDataRecord(myReader));
                        }
                    }
                    myReader.Close();
                }
            }
        }
        return tempList;
    }

    // Inserts an item in the request table
    public static int Insert(Request myRequest)
    {
        int result = 0;
        using (SqlConnection myConnection = new SqlConnection(AppConfiguration.SqlConnectionString))
        {
            using (SqlCommand myCommand = new SqlCommand("sprocRequestInsertItem", myConnection))
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(myRequest.CreatedBy))
                {
                    myCommand.Parameters.AddWithValue("@CreatedBy", myRequest.CreatedBy);
                }
                if (!string.IsNullOrEmpty(myRequest.AssignedDepartment))
                {
                    myCommand.Parameters.AddWithValue("@AssignedDepartment", myRequest.AssignedDepartment);
                }
                if (!string.IsNullOrEmpty(myRequest.RequestStatus))
                {
                    myCommand.Parameters.AddWithValue("@RequestStatus", myRequest.RequestStatus);
                }
                if (!string.IsNullOrEmpty(myRequest.Comment))
                {
                    myCommand.Parameters.AddWithValue("@Comment", myRequest.Comment);
                }
                if (!string.IsNullOrEmpty(myRequest.CaseName))
                {
                    myCommand.Parameters.AddWithValue("@CaseName", myRequest.CaseName);
                }
                if (!string.IsNullOrEmpty(myRequest.TicketNumber))
                {
                    myCommand.Parameters.AddWithValue("@TicketNumber", myRequest.TicketNumber);
                }
                if (!string.IsNullOrEmpty(myRequest.CouponNumber))
                {
                    myCommand.Parameters.AddWithValue("@CouponNumber", myRequest.CouponNumber);
                }
                if (!string.IsNullOrEmpty(myRequest.PNR))
                {
                    myCommand.Parameters.AddWithValue("@PNR", myRequest.PNR);
                }
                if (!string.IsNullOrEmpty(myRequest.FlightNumber))
                {
                    myCommand.Parameters.AddWithValue("@FlightNumber", myRequest.FlightNumber);
                }
                if (myRequest.FlightDate != DateTime.MinValue)
                {
                    myCommand.Parameters.AddWithValue("@FlightDate", myRequest.FlightDate);
                }
                if (!string.IsNullOrEmpty(myRequest.DepartureSector))
                {
                    myCommand.Parameters.AddWithValue("@DepartureSector", myRequest.DepartureSector);
                }
                if (!string.IsNullOrEmpty(myRequest.ArrivalSector))
                {
                    myCommand.Parameters.AddWithValue("@ArrivalSector", myRequest.ArrivalSector);
                }
                if (!string.IsNullOrEmpty(myRequest.TicketCabin))
                {
                    myCommand.Parameters.AddWithValue("@TicketCabin", myRequest.TicketCabin);
                }
                if (!string.IsNullOrEmpty(myRequest.TravelCabin))
                {
                    myCommand.Parameters.AddWithValue("@TravelCabin", myRequest.TravelCabin);
                }
                if (!string.IsNullOrEmpty(myRequest.SeatNumber))
                {
                    myCommand.Parameters.AddWithValue("@SeatNumber", myRequest.SeatNumber);
                }
                if (!string.IsNullOrEmpty(myRequest.Justification))
                {
                    myCommand.Parameters.AddWithValue("@Justification", myRequest.Justification);
                }
                if (!string.IsNullOrEmpty(myRequest.PassengerMobile))
                {
                    myCommand.Parameters.AddWithValue("@PassengerMobile", myRequest.PassengerMobile);
                }
                if (!string.IsNullOrEmpty(myRequest.PassengerEmail))
                {
                    myCommand.Parameters.AddWithValue("@PassengerEmail", myRequest.PassengerEmail);
                }
                if (!string.IsNullOrEmpty(myRequest.TicketMobile))
                {
                    myCommand.Parameters.AddWithValue("@TicketMobile", myRequest.TicketMobile);
                }
                myConnection.Open();
                object output = myCommand.ExecuteScalar();
                if (output != null)
                    result = (int)output;
            }
            myConnection.Close();
        }
        return result;
    }

    public static bool Update(Request myRequest)
    {
        int result = 0;
        using (SqlConnection myConnection = new SqlConnection(AppConfiguration.SqlConnectionString))
        {
            using (SqlCommand myCommand = new SqlCommand("sprocRequestUpdateItem", myConnection))
            {
                myCommand.CommandType = CommandType.StoredProcedure;

                myCommand.Parameters.AddWithValue("@RequestID", myRequest.RequestID);
                if (!string.IsNullOrEmpty(myRequest.CreatedBy))
                {
                    myCommand.Parameters.AddWithValue("@CreatedBy", myRequest.CreatedBy);
                }
                if (!string.IsNullOrEmpty(myRequest.ClosedBy))
                {
                    myCommand.Parameters.AddWithValue("@ClosedBy", myRequest.ClosedBy);
                }
                if (myRequest.CreateDate != DateTime.MinValue)
                {
                    myCommand.Parameters.AddWithValue("@CreateDate", myRequest.CreateDate);
                }
                if (myRequest.CloseDate != DateTime.MinValue)
                {
                    myCommand.Parameters.AddWithValue("@CloseDate", myRequest.CloseDate);
                }
                if (!string.IsNullOrEmpty(myRequest.AssignedUser))
                {
                    myCommand.Parameters.AddWithValue("@AssignedUser", myRequest.AssignedUser);
                }
                if (!string.IsNullOrEmpty(myRequest.AssignedDepartment))
                {
                    myCommand.Parameters.AddWithValue("@AssignedDepartment", myRequest.AssignedDepartment);
                }
                if (!string.IsNullOrEmpty(myRequest.RequestStatus))
                {
                    myCommand.Parameters.AddWithValue("@RequestStatus", myRequest.RequestStatus);
                }
                if (!string.IsNullOrEmpty(myRequest.Comment))
                {
                    myCommand.Parameters.AddWithValue("@Comment", myRequest.Comment);
                }
                if (!string.IsNullOrEmpty(myRequest.CaseName))
                {
                    myCommand.Parameters.AddWithValue("@CaseName", myRequest.CaseName);
                }
                if (!string.IsNullOrEmpty(myRequest.TicketNumber))
                {
                    myCommand.Parameters.AddWithValue("@TicketNumber", myRequest.TicketNumber);
                }
                if (!string.IsNullOrEmpty(myRequest.CouponNumber))
                {
                    myCommand.Parameters.AddWithValue("@CouponNumber", myRequest.CouponNumber);
                }
                if (!string.IsNullOrEmpty(myRequest.PNR))
                {
                    myCommand.Parameters.AddWithValue("@PNR", myRequest.PNR);
                }
                if (!string.IsNullOrEmpty(myRequest.FlightNumber))
                {
                    myCommand.Parameters.AddWithValue("@FlightNumber", myRequest.FlightNumber);
                }
                if (myRequest.FlightDate != DateTime.MinValue)
                {
                    myCommand.Parameters.AddWithValue("@FlightDate", myRequest.FlightDate);
                }
                if (!string.IsNullOrEmpty(myRequest.DepartureSector))
                {
                    myCommand.Parameters.AddWithValue("@DepartureSector", myRequest.DepartureSector);
                }
                if (!string.IsNullOrEmpty(myRequest.ArrivalSector))
                {
                    myCommand.Parameters.AddWithValue("@ArrivalSector", myRequest.ArrivalSector);
                }
                if (!string.IsNullOrEmpty(myRequest.TicketCabin))
                {
                    myCommand.Parameters.AddWithValue("@TicketCabin", myRequest.TicketCabin);
                }
                if (!string.IsNullOrEmpty(myRequest.TravelCabin))
                {
                    myCommand.Parameters.AddWithValue("@TravelCabin", myRequest.TravelCabin);
                }
                if (!string.IsNullOrEmpty(myRequest.SeatNumber))
                {
                    myCommand.Parameters.AddWithValue("@SeatNumber", myRequest.SeatNumber);
                }
                if (!string.IsNullOrEmpty(myRequest.Justification))
                {
                    myCommand.Parameters.AddWithValue("@Justification", myRequest.Justification);
                }
                if (!string.IsNullOrEmpty(myRequest.PassengerMobile))
                {
                    myCommand.Parameters.AddWithValue("@PassengerMobile", myRequest.PassengerMobile);
                }
                if (!string.IsNullOrEmpty(myRequest.PassengerEmail))
                {
                    myCommand.Parameters.AddWithValue("@PassengerEmail", myRequest.PassengerEmail);
                }
                if (!string.IsNullOrEmpty(myRequest.CompensationEMD))
                {
                    myCommand.Parameters.AddWithValue("@CompensationEMD", myRequest.CompensationEMD);
                }
                myCommand.Parameters.AddWithValue("@CompensationAmount", myRequest.CompensationAmount);
                if (!string.IsNullOrEmpty(myRequest.DifferenceEMD))
                {
                    myCommand.Parameters.AddWithValue("@DifferenceEMD", myRequest.DifferenceEMD);
                }
                myCommand.Parameters.AddWithValue("@DifferenceAmount", myRequest.DifferenceAmount);

                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        return result > 0;
    }

    public static bool Delete(int RequestID)
    {
        int result = 0;
        using (SqlConnection myConnection = new SqlConnection(AppConfiguration.SqlConnectionString))
        {
            using (SqlCommand myCommand = new SqlCommand("sprocRequestDeleteItem", myConnection))
            {
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.Parameters.AddWithValue("@RequestID", RequestID);
                myConnection.Open();
                result = myCommand.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        return result > 0;
    }

    #endregion

    #region Private methods

    private static Request FillDataRecord(IDataRecord myDataRecord)
    {
        Request myRequest = new Request();
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("RequestID")))
        {
            myRequest.RequestID = myDataRecord.GetInt32(myDataRecord.GetOrdinal("RequestID"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CreatedBy")))
        {
            myRequest.CreatedBy = myDataRecord.GetString(myDataRecord.GetOrdinal("CreatedBy"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ClosedBy")))
        {
            myRequest.ClosedBy = myDataRecord.GetString(myDataRecord.GetOrdinal("ClosedBy"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CreateDate")))
        {
            myRequest.CreateDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("CreateDate"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CloseDate")))
        {
            myRequest.CloseDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("CloseDate"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AssignedUser")))
        {
            myRequest.AssignedUser = myDataRecord.GetString(myDataRecord.GetOrdinal("AssignedUser"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("AssignedDepartment")))
        {
            myRequest.AssignedDepartment = myDataRecord.GetString(myDataRecord.GetOrdinal("AssignedDepartment"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("RequestStatus")))
        {
            myRequest.RequestStatus = myDataRecord.GetString(myDataRecord.GetOrdinal("RequestStatus"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Comment")))
        {
            myRequest.Comment = myDataRecord.GetString(myDataRecord.GetOrdinal("Comment"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CaseName")))
        {
            myRequest.CaseName = myDataRecord.GetString(myDataRecord.GetOrdinal("CaseName"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("TicketNumber")))
        {
            myRequest.TicketNumber = myDataRecord.GetString(myDataRecord.GetOrdinal("TicketNumber"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CouponNumber")))
        {
            myRequest.CouponNumber = myDataRecord.GetString(myDataRecord.GetOrdinal("CouponNumber"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("PNR")))
        {
            myRequest.PNR = myDataRecord.GetString(myDataRecord.GetOrdinal("PNR"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FlightNumber")))
        {
            myRequest.FlightNumber = myDataRecord.GetString(myDataRecord.GetOrdinal("FlightNumber"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("FlightDate")))
        {
            myRequest.FlightDate = myDataRecord.GetDateTime(myDataRecord.GetOrdinal("FlightDate"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DepartureSector")))
        {
            myRequest.DepartureSector = myDataRecord.GetString(myDataRecord.GetOrdinal("DepartureSector"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("ArrivalSector")))
        {
            myRequest.ArrivalSector = myDataRecord.GetString(myDataRecord.GetOrdinal("ArrivalSector"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("TicketCabin")))
        {
            myRequest.TicketCabin = myDataRecord.GetString(myDataRecord.GetOrdinal("TicketCabin"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("TravelCabin")))
        {
            myRequest.TravelCabin = myDataRecord.GetString(myDataRecord.GetOrdinal("TravelCabin"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("SeatNumber")))
        {
            myRequest.SeatNumber = myDataRecord.GetString(myDataRecord.GetOrdinal("SeatNumber"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("Justification")))
        {
            myRequest.Justification = myDataRecord.GetString(myDataRecord.GetOrdinal("Justification"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("PassengerMobile")))
        {
            myRequest.PassengerMobile = myDataRecord.GetString(myDataRecord.GetOrdinal("PassengerMobile"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("PassengerEmail")))
        {
            myRequest.PassengerEmail = myDataRecord.GetString(myDataRecord.GetOrdinal("PassengerEmail"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CompensationEMD")))
        {
            myRequest.CompensationEMD = myDataRecord.GetString(myDataRecord.GetOrdinal("CompensationEMD"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("CompensationAmount")))
        {
            myRequest.CompensationAmount = myDataRecord.GetInt32(myDataRecord.GetOrdinal("CompensationAmount"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DifferenceEMD")))
        {
            myRequest.DifferenceEMD = myDataRecord.GetString(myDataRecord.GetOrdinal("DifferenceEMD"));
        }
        if (!myDataRecord.IsDBNull(myDataRecord.GetOrdinal("DifferenceAmount")))
        {
            myRequest.DifferenceAmount = myDataRecord.GetInt32(myDataRecord.GetOrdinal("DifferenceAmount"));
        }
        return myRequest;
    }

    #endregion
}