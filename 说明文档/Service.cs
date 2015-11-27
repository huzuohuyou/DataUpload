﻿
using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Serialization;


/// <summary>
///青岛大学医学院附属医院
/// 本接口支持数据库为SQL SERVER2000或者更高版本
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[XmlRoot(Namespace = "", IsNullable = false, ElementName = "RequestResult")]
public class EmrService : System.Web.Services.WebService
{
    public EmrService()
    {
        //InitializeVariables();
        //JHEMR.EmrSysDAL.DALUse.SetConnectString("SQLSERVER", ConfigurationManager.ConnectionStrings["EMRConnectionString"].ConnectionString);
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "123123";
    }

    [WebMethod]
    public string CallInterface(string p_strXml, string p_strDocumentId)
    {
        p_strXml = System.Text.Encoding.UTF8.GetString(System.Text.Encoding.UTF8.GetBytes(p_strXml));
        string _strTemp = "<ProvideAndRegisterDocumentSetRequest xmlns=\"urn:hl7-org:v3\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"urn:hl7-org:v3 ../multicacheschemas/ProvideAndRegisterDocumentSetRequest.xsd\">";
        _strTemp += "<ID root=\"EMR\" extension=\"UID\"/>";
        _strTemp += "<SourcePatientID>patient_id</SourcePatientID>";
        _strTemp += "<SourcePatientName>name</SourcePatientName>";
        _strTemp += "<HealthCardId>健康卡号</HealthCardId>";
        _strTemp += "<IdentityId>ID_NO</IdentityId>";
        _strTemp += "<Organization id=\"40001401-4\">";
        _strTemp += "<Name>中日友好医院</Name>";
        _strTemp += "<TelephoneNumber areaCode=\"010\" number=\"组织机构电话\"/>";
        _strTemp += "<EmailAddress address=\"组织机构邮箱\"/>";
        _strTemp += "<Address city=\"工作省\" country=\"工作国\" postalCode=\"工作地址县\" stateOrProvince=\"工作地址乡\" street=\"工作地址街道工作地址号码\"/>";
        _strTemp += "</Organization>";
        _strTemp += "<RegistryPackage>";
        _strTemp += "<SubmissionSet targetObject=\"Document.1\" availabilityStatus=\"Submitted\">";
        _strTemp += "<SubmissionTime>sysdateyyyymmddHHMISS</SubmissionTime>";
        _strTemp += "<UniqueId>patient_ID||VISIT_ID||FILE_NO</UniqueId>";
        _strTemp += "<SourceId>源ID？？？</SourceId>";
        _strTemp += "<Comments>备注？？？</Comments>";
        _strTemp += "<Title>住院记录</Title>";
        _strTemp += "<CreateTime>Create_date_timeyyyymmddHHMISS</CreateTime>";
        _strTemp += "<ServerOrganization>服务机构名称？？？</ServerOrganization>";
        _strTemp += "<EpisodeID>inp_no</EpisodeID>";
        _strTemp += "<InTime>Admission_date_timeyyyymmddHHMISS</InTime>";
        _strTemp += "<OutTime>discharge_date_timeyyyymmddHHMISS</OutTime>";
        _strTemp += "<AdmissionDepart>dept_name</AdmissionDepart>";
        _strTemp += "<AdmissionDoctor>parent_name</AdmissionDoctor>";
        _strTemp += "<AdmissionType>就诊类型？？？？</AdmissionType>";
        _strTemp += "<DiagnosisResult>diagnosis_desc</DiagnosisResult>";
        _strTemp += "<Author>";
        _strTemp += "<AuthorName>super_name</AuthorName>";
        _strTemp += "<AuthorInstitution>专家所属机构？？？？</AuthorInstitution>";
        _strTemp += "<AuthorSpecialty>科室</AuthorSpecialty>";
        _strTemp += "<AuthorRole>主任医师</AuthorRole>";
        _strTemp += "</Author>";
        _strTemp += "</SubmissionSet>";
        _strTemp += "</RegistryPackage>";
        _strTemp += "<Document id=\"{1}\" mimeType=\"xml\" parentDocumentRelationship=\"文档关系\" parentDocumentId=\"上级文档ID\">";
        _strTemp += "<Content>{0}</Content>";
        _strTemp += "</Document>";
        _strTemp += "</ProvideAndRegisterDocumentSetRequest>";
        string _strResult = string.Format(_strTemp, p_strXml, p_strDocumentId);
        return "123";
    }

    
    
    
}
