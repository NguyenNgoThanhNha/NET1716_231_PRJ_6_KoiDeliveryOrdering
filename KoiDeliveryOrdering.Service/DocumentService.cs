﻿using KoiDeliveryOrdering.Business.Base;
using KoiDeliveryOrdering.Business.Interfaces;
using KoiDeliveryOrdering.Common;
using KoiDeliveryOrdering.Data;
using KoiDeliveryOrdering.Data.Dtos.Documents;
using KoiDeliveryOrdering.Data.Entities;
using KoiDeliveryOrdering.Service.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrdering.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly UnitOfWork _unitOfWork;

        public DocumentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult> GetAll()
        {
            try
            {
                var documents = await _unitOfWork.DocumentRepository.FindAll(false).ToListAsync();
                return new ServiceResult(Const.SUCCESS_READ_CODE, Const.SUCCESS_READ_MSG, documents.ToList());
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<ServiceResult> CreateDocument(DocumentMutationDto dto)
        {
            try
            {
                decimal totalShippingFee = 0;
                dto.DocumentDetails.ForEach(dd =>
                {
                    dd.ItemEstimatePrice = dd.ItemQuantity * dd.ItemWeight * Const.PRICE_PER_KILOGAM;
                    totalShippingFee += dd.ItemEstimatePrice;
                });
                var entity = dto.Adapt<Document>();
                entity.ShippingFee = totalShippingFee;
                entity.DocumentId = new Guid();
                entity.DocumentNumber = "DOC" + ((long)new Random().Next(100000000, 1000000000)).ToString();
                _unitOfWork.DocumentRepository.Create(entity);
                await _unitOfWork.SaveAsync();
                Console.WriteLine(entity);
                return new ServiceResult(Const.SUCCESS_INSERT_CODE, Const.SUCCESS_INSERT_MSG);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }

        public async Task<ServiceResult> UpdateDocument(Guid id, DocumentMutationDto dto)
        {
            try
            {
                var entity = await _unitOfWork.DocumentRepository.FindByCondition(d => d.DocumentId == id, true)
                    .FirstOrDefaultAsync();
                if (entity is null)
                    return new ServiceResult(Const.FAIL_UPDATE_CODE, Const.FAIL_UPDATE_MSG);

                dto.Adapt(entity);
                await _unitOfWork.SaveAsync();
                return new ServiceResult(Const.SUCCESS_UPDATE_CODE, Const.SUCCESS_UPDATE_MSG);
            }
            catch (Exception ex)
            {
                return new ServiceResult(Const.ERROR_EXCEPTION_CODE, ex.Message);
            }
        }
    }
}