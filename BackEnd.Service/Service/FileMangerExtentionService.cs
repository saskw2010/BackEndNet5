using AutoMapper;
using BackEnd.BAL.Interfaces;
using BackEnd.BAL.Models;
using BackEnd.DAL.Entities;
using BackEnd.Service.IService;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Service.Service
{
    public class FileMangerExtentionService : IFileMangerExtentionService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FileMangerExtentionService(IUnitOfWork unitOfWork,
          IMapper mapper
          )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result> addFileManagersByextention(FileMangerExtentionViewModel fileMangerExtentionViewModel)
        {
            try 
            {
                fileManagerExtentions FileExtention = new fileManagerExtentions();
                var obj = _mapper.Map(fileMangerExtentionViewModel, FileExtention);
                
                _unitOfWork.FileManagerExtentionRepository.Insert(obj);
             var res=  await _unitOfWork.SaveAsync();
                if (res == 200) {
                    return new Result { success = true, code = "200", message = "Extention" };
                }
                else
                {
                    return new Result
                    {
                        success = false,
                        code = "403",
                        message = "row updated Falid"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result
                {
                    success = false,
                    code = "400",
                    data = null,
                    message = ExtensionMethods.FullMessage(ex)
                };
            }
        }

        public Result delete(int id)
        {
            try
            {
                _unitOfWork.FileManagerExtentionRepository.Delete(id);
                var res =  _unitOfWork.Save();
                if (res == 200)
                {
                    return new Result { success = true, code = "200", message = "Id Is Delete" };
                }
                else
                {
                    return new Result
                    {
                        success = false,
                        code = "403",
                        message = "row delete Falid"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result
                {
                    success = false,
                    code = "400",
                    data = null,
                    message = ExtensionMethods.FullMessage(ex)
                };
            }

            //return new Result { message = "Id Is Delete" };
        }

        public Result GetAllFileManagersextention()
        {
            try
            {

                var fileManagerExtenstioList = _unitOfWork.FileManagerExtentionRepository.Get().ToList();
                var FilemanagerExtestionVmList = _mapper.Map<List<FileMangerExtentionViewModel>>(fileManagerExtenstioList);
                return new Result
                {
                    success = true,
                    code = "200",
                    data = FilemanagerExtestionVmList
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    success = false,
                    code = "403",
                    message = ExtensionMethods.FullMessage(ex),
                    data = null
                };
            }

        }

        public Result GetAllFileManagersextention(int id)
        {

            try
            {

                var fileManagerExtenstion = _unitOfWork.FileManagerExtentionRepository.GetByID(id);
                var FilemanagerExtestionVm = _mapper.Map<FileMangerExtentionViewModel>(fileManagerExtenstion);
                return new Result
                {
                    success = true,
                    code = "200",
                    data = FilemanagerExtestionVm
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    success = false,
                    code = "403",
                    message = ExtensionMethods.FullMessage(ex),
                    data = null
                };
            }
        }

        public async Task<Result> UpdateFileManagersByextention(FileMangerExtentionViewModel fileMangerExtentionViewModel)
        {
            try
            {
                fileManagerExtentions FileExtention = new fileManagerExtentions();
                var obj = _mapper.Map(fileMangerExtentionViewModel, FileExtention);

                _unitOfWork.FileManagerExtentionRepository.Update(obj);
                var res = await _unitOfWork.SaveAsync();
                if (res == 200)
                {
                    return new Result { success = true, code = "200", message = "Extention" };
                }
                else
                {
                    return new Result
                    {
                        success = false,
                        code = "403",
                        message = "row updated Falid"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result
                {
                    success = false,
                    code = "400",
                    data = null,
                    message = ExtensionMethods.FullMessage(ex)
                };
            }
        }
    }
}
