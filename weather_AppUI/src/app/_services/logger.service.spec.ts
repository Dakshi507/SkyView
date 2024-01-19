import { TestBed } from '@angular/core/testing';
import { LoggerService } from './logger.service';

describe('LoggerService', () => {
  let service: LoggerService;
  let consoleSpy: jasmine.Spy;
  let consoleDebugSpy: jasmine.Spy;
  let consoleWarnSpy: jasmine.Spy;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LoggerService);
    consoleDebugSpy = spyOn(console, 'debug');
   
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should call console.error with error when error is called with an error', () => {
    const errorSpy = spyOn(console, 'error');
    const mockError = new Error('Test Error');
    service.error('Testing Error Message', mockError);
    expect(errorSpy).toHaveBeenCalledWith(jasmine.any(String), mockError);
  });

  it('should call console.debug when debug is called', () => {
    service.debug('Testing Debug Message');
    expect(consoleDebugSpy).toHaveBeenCalled();
  });

 

});
