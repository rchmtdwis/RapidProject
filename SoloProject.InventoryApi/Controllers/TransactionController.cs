﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoloProject.InventoryApi.DTOs;
using SoloProject.InventoryApi.Models;
using SoloProject.InventoryApi.Repositories;

namespace SoloProject.InventoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionServices;
        private readonly IProductRepository _productServices;

        public TransactionController(ITransactionRepository transactionServices, IProductRepository productServices)
        {
            _transactionServices = transactionServices;
            _productServices = productServices;
        }
        [HttpPost]
        public async Task<ActionResult<TransactionDTO>> CreateTransaction(CreateTransactionDTO createTransactionDTO)
        {
            try
            {
                var transType = createTransactionDTO.TransactionType;
                Transaction newTrans = new Transaction
                {
                    ProductId = createTransactionDTO.ProductId,
                    TransactionType = GetTransactionType(transType), // Ensure this method exists and works as expected
                    Quantity = createTransactionDTO.Quantity,
                };

                var transaction = await _transactionServices.Add(newTrans);
                var product = await _productServices.GetById(newTrans.ProductId);
                if (transaction == null)
                {
                    return BadRequest("Transaction could not be created.");
                }

                TransactionDTO transactionDTO = new TransactionDTO
                {
                    TransactionId = transaction.TransactionId,
                    ProductId = transaction.ProductId,
                    TransactionType = transType,
                    Quantity = createTransactionDTO.Quantity,
                    Product = new ProductDTO
                    {
                        Id = product.Id,
                        Code = product.Code,
                        Name = product.Name,
                        Stock = product.Stock,
                        Price = product.Price
                    }
                };

                return Ok(transactionDTO);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private Boolean GetTransactionType(string TransType)
            {
                if(TransType == "Add")
                {
                    return true;
                } else if(TransType == "Remove")
                {
                    return false;
                }
                else
                {
                    throw new Exception("Tipe transaksi tidak ada");
                }

            }
        }
}