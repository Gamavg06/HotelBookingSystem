using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // ¡NUEVO! Necesario para FirstOrDefaultAsync
using HotelReservation.Data;
using HotelReservation.Models;
using HotelReservation.Services;

namespace HotelReservation.Pages
{
    public class PaymentModel : PageModel
    {
        // CAMBIO 1: Reemplazar InMemoryStore por HotelDbContext
        private readonly HotelDbContext _context; 
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public Room SelectedRoom { get; set; }

        [BindProperty]
        public PaymentInput Input { get; set; } = new();

        // CAMBIO 2: Actualizar el constructor
        public PaymentModel(HotelDbContext context, IEmailService emailService, IConfiguration config)
        {
            _context = context;
            _emailService = emailService;
            _config = config;
        }

        public class PaymentInput
        {
            public string PaymentMethod { get; set; } = string.Empty;
            public string CardName { get; set; } = string.Empty;
            public string CardNumber { get; set; } = string.Empty;
            public string ExpirationDate { get; set; } = string.Empty;
            public string CVV { get; set; } = string.Empty;
            public string BankName { get; set; } = string.Empty;
            public string AccountNumber { get; set; } = string.Empty;
            public string PayPalEmail { get; set; } = string.Empty;
        }

        // CAMBIO 3: Hacer el método asíncrono
        public async Task<IActionResult> OnGetAsync() 
        {
            var roomIdStr = HttpContext.Session.GetString("RoomId");
            if (!int.TryParse(roomIdStr, out var roomId))
            {
                TempData["ErrorMessage"] = "No hay habitación seleccionada.";
                return RedirectToPage("/Rooms");
            }

            // CAMBIO 4: Usar FirstOrDefaultAsync
            SelectedRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId); 
            
            if (SelectedRoom == null)
            {
                TempData["ErrorMessage"] = "Habitación no encontrada.";
                return RedirectToPage("/Rooms");
            }

            return Page();
        }

        // CAMBIO 5: Hacer el método asíncrono
        public async Task<IActionResult> OnPostAsync()
        {
            var roomIdStr = HttpContext.Session.GetString("RoomId");
            if (!int.TryParse(roomIdStr, out var roomId))
            {
                TempData["ErrorMessage"] = "No hay habitación seleccionada.";
                return RedirectToPage("/Rooms");
            }

            // CAMBIO 6: Usar FirstOrDefaultAsync
            SelectedRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            
            if (SelectedRoom == null)
            {
                TempData["ErrorMessage"] = "Habitación no encontrada.";
                return RedirectToPage("/Rooms");
            }

            if (string.IsNullOrWhiteSpace(Input.PaymentMethod))
            {
                ModelState.AddModelError(string.Empty, "Selecciona un método de pago.");
                return Page();
            }

            // validaciones simples
            if (Input.PaymentMethod == "tarjeta")
            {
                if (string.IsNullOrWhiteSpace(Input.CardNumber) || string.IsNullOrWhiteSpace(Input.ExpirationDate) || string.IsNullOrWhiteSpace(Input.CVV))
                {
                    ModelState.AddModelError(string.Empty, "Completa los datos de la tarjeta.");
                    return Page();
                }
            }
            else if (Input.PaymentMethod == "transferencia")
            {
                if (string.IsNullOrWhiteSpace(Input.BankName) || string.IsNullOrWhiteSpace(Input.AccountNumber))
                {
                    ModelState.AddModelError(string.Empty, "Completa los datos de la transferencia.");
                    return Page();
                }
            }
            else if (Input.PaymentMethod == "paypal")
            {
                if (string.IsNullOrWhiteSpace(Input.PayPalEmail))
                {
                    ModelState.AddModelError(string.Empty, "Ingresa tu correo PayPal.");
                    return Page();
                }
            }

            // Guardar datos de pago/reserva en sesión para Confirm
            HttpContext.Session.SetString("PaymentMethod", Input.PaymentMethod);
            if (!string.IsNullOrEmpty(Input.CardName)) HttpContext.Session.SetString("CardName", Input.CardName);
            if (!string.IsNullOrEmpty(Input.PayPalEmail)) HttpContext.Session.SetString("PayPalEmail", Input.PayPalEmail);

            // Preparar correo HTML (profesional)
            var nights = 1; // si quieres calcular nights, recupera fechas desde sesión
            var total = SelectedRoom.PricePerNight * nights;

            var userEmail = HttpContext.Session.GetString("UserEmail") ?? Input.PayPalEmail ?? "";

            var ownerEmail = _config["EmailSettings:OwnerEmail"] ?? _config["EmailSettings:SenderEmail"];

            var subject = $"Reserva simulada - {SelectedRoom.Name}";
            var body = $@"
                <div style='font-family:Arial,Helvetica,sans-serif;color:#333;'>
                    <div style='background:#c9a227;padding:18px;border-radius:8px;color:#fff'>
                        <h2 style='margin:0'>Hotel Reserva - Confirmación (Simulada)</h2>
                    </div>

                    <div style='padding:18px'>
                        <h3>Detalles de la reserva</h3>
                        <p><strong>Habitación:</strong> {SelectedRoom.Name}</p>
                        <p><strong>Precio por noche:</strong> ${SelectedRoom.PricePerNight:0.00}</p>
                        <p><strong>Total (simulado):</strong> ${total:0.00}</p>
                        <p><strong>Método:</strong> {Input.PaymentMethod}</p>
                        <hr />
                        <p>Fecha: {DateTime.Now}</p>
                    </div>

                    <div style='padding:18px;color:#666;font-size:12px'>
                        <p>Este es un pago simulado para pruebas.</p>
                    </div>
                </div>
            ";

            // Enviar correo (si está configurado)
            if (_emailService.CanSend())
            {
                // enviar a owner y user (si existe)
                try
                {
                    if (!string.IsNullOrWhiteSpace(ownerEmail))
                        await _emailService.SendEmailAsync(ownerEmail, subject, body);

                    if (!string.IsNullOrWhiteSpace(userEmail))
                        await _emailService.SendEmailAsync(userEmail, subject, body);
                }
                catch (Exception ex)
                {
                    // no bloquear el flujo por error de correo; mostrar aviso
                    TempData["ErrorMessage"] = "Error enviando correo (comprueba configuración).";
                }
            }
            else
            {
                // Si no está configurado, guardar preview para mostrar en pantalla (útil en local)
                TempData["EmailPreview"] = body;
            }

            TempData["PaymentDone"] = "true";
            return RedirectToPage("Confirm");
        }
    }
}  