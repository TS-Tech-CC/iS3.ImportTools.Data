using DataEntryService.WebAPI.model;
using System;
using System.Collections.Generic;
using System.Web.Http;


namespace DataEntryService.WebAPI.controllers
{
    [RoutePrefix("api/demo")]
    public class DemoController : ApiController
    {
        [HttpGet]
        [Route("Server")]
        public bool StartServer()
        {
            return Service1.StartOneThread();
        }



        [HttpGet]
        [Route("student")]
        public dynamic allStudents()
        {
            return new
            {
                result = true,
                desc = "请求成功",
                data = new List<StudentInfo>()
                {
                    new StudentInfo
                    {
                        ClassId="ClassID982712311231",
                        Sex="男",
                        StudentId=Guid.NewGuid().ToString(),
                        StudentName="男学生一枚",
                        StudentNumber="XH2018090001"
                    },
                    new StudentInfo
                    {
                        ClassId="ClassID982712311232",
                        Sex="女",
                        StudentId=Guid.NewGuid().ToString(),
                        StudentName="女学生一枚",
                        StudentNumber="XH2018090002"
                    }
                }
            };
        }



        [HttpGet]
        [Route("student")]
        public dynamic queryStudents(string name)
        {
            return new
            {
                result = true,
                desc = "查询请求成功,参数name:" + name,
                data = new List<StudentInfo>()
                {
                    new StudentInfo
                    {
                        ClassId="ClassID982712311231",
                        Sex="男",
                        StudentId=Guid.NewGuid().ToString(),
                        StudentName="男学生一枚",
                        StudentNumber="XH2018090001"
                    },
                    new StudentInfo
                    {
                        ClassId="ClassID982712311232",
                        Sex="女",
                        StudentId=Guid.NewGuid().ToString(),
                        StudentName="女学生一枚",
                        StudentNumber="XH2018090002"
                    }
                },
                page = 1,
                total = 2,
                pageCount = 1
            };
        }


        [HttpPost]
        [Route("student")]
        public dynamic saveStudents([FromBody]StudentInfo info)
        {
            info.StudentId = Guid.NewGuid().ToString();
            return new
            {
                result = true,
                desc = "保存成功,学生姓名:" + info.StudentName,
                data = info.StudentId
            };
        }


    }
}
